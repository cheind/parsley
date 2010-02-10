using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace Parsley.Core.Addins {

  /// <summary>
  /// Used as TypeConverter in a PropertyGrid to populate drop-down with
  /// a list of classes that are assignable to the property type.
  /// <remarks>When the property changes, the new type is instanciated and
  /// assigned to the property. The property should be assigned the 
  /// [RefreshProperties(RefreshProperties.All)] attribute in order to refresh on change.</remarks>
  /// </summary>
  public class ReflectionTypeConverter : ExpandableObjectConverter {
    private Type _type_of;
    private Dictionary<string, Core.Addins.AddinInfo> _addin_dict;

    /// <summary>
    /// Initialize with type to reflect
    /// </summary>
    /// <param name="type_of"></param>
    public ReflectionTypeConverter(Type type_of) {
      _type_of = type_of;
    }

    /// <summary>
    /// Converts the given value to the type of this converter.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="culture"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public override object ConvertFrom(
      System.ComponentModel.ITypeDescriptorContext context, 
      System.Globalization.CultureInfo culture, 
      object value) 
    {
      if (value is string) {
        Addins.AddinInfo addin = _addin_dict[(string)value];
        return Addins.AddinStore.CreateInstance(addin);
      } else {
        return base.ConvertFrom(context, culture, value);
      }
    }

    /// <summary>
    /// Converts the given value object to the specified type, using the arguments.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="culture"></param>
    /// <param name="value"></param>
    /// <param name="destType"></param>
    /// <returns></returns>
    public override object ConvertTo(
           ITypeDescriptorContext context,
           CultureInfo culture,
           object value,
           Type destType) 
    {
      if (destType == typeof(string)) {
        return value.GetType().FullName;
      }
      return base.ConvertTo(context, culture, value, destType);
    }


    /// <summary>
    /// Returns whether this object supports a standard set of values that can be picked from a list.
    /// </summary>
    public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context) {
      return true;
    }

    /// <summary>
    /// Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.
    /// </summary>
    public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context) {
      IEnumerable<Addins.AddinInfo> t = Addins.AddinStore.FindAddins(_type_of, ai => ai.DefaultConstructible);
      _addin_dict = t.ToDictionary(ai => ai.FullName);
      return new StandardValuesCollection(t.ToArray());
    }

    /// <summary>
    /// Returns whether this converter can convert an object of the given type to the type of this converter.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sourceType"></param>
    /// <returns></returns>
    public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType) {
      return sourceType == typeof(string);
    }
  }
}
