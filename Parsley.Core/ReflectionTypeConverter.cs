using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Parsley.Core {

  /// <summary>
  /// Used as TypeConverter in a PropertyGrid to populate drop-down with
  /// a list of classes that are assignable to the property type.
  /// <remarks>When the property changes, the new type is instanciated and
  /// assigned to the property. The property should be assigned the 
  /// [RefreshProperties(RefreshProperties.All)] attribute in order to refresh on change.</remarks>
  /// </summary>
  public class ReflectionTypeConverter : ExpandableObjectConverter {
    private Type _type_of;
    private Dictionary<string, Core.Addins.AddinInfo> _type_dict;


    public ReflectionTypeConverter(Type type_of) {
      _type_of = type_of;
    }

    public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {
      if (value.GetType() == typeof(string)) {
        return Addins.AddinStore.CreateInstance(_type_dict[(string)value]);
      } else
        return base.ConvertFrom(context, culture, value);
    }


    public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context) {
      return true;
    }

    public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context) {
      IEnumerable<Addins.AddinInfo> t = Addins.AddinStore.FindAddins(_type_of, ai => ai.DefaultConstructible);
      _type_dict = t.ToDictionary(ai => ai.FullName);
      return new StandardValuesCollection(t.ToArray());
    }

    public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType) {
      if (sourceType == typeof(string))
        return true;
      else
        return base.CanConvertFrom(context, sourceType);
    }
  }
}
