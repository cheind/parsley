#pragma once

#include <osg/ref_ptr>

namespace Parsley {
  namespace Draw3D {

    /// CLI type that acts as an auto_ptr on osg types.
    template<class T>
    public ref class auto_ptr_osg {
    public:
      typedef osg::ref_ptr<T> osg_ptr;
      typedef auto_ptr_osg<T> self_type;

      auto_ptr_osg() : _ptr(new osg_ptr()) {}
      explicit auto_ptr_osg(T *native) : _ptr(new osg_ptr(native)) {}
      auto_ptr_osg(self_type %o) : _ptr(new osg_ptr(o.osg())) {}
      template<class S> auto_ptr_osg(auto_ptr_osg<S> %o) : _ptr(new osg_ptr(o.osg())) {}
      ~auto_ptr_osg() { System::Console::WriteLine("dispose!");delete _ptr; }
      !auto_ptr_osg() { System::Console::WriteLine("finalize!");delete _ptr; }

      self_type %operator=(self_type %rhs) {
        (*_ptr) = rhs.osg();
        return *this;
      }

      template<class S>
      self_type %operator=(auto_ptr_osg<S> %rhs) {
        (*_ptr) = rhs.osg();
        return *this;
      }

      self_type %operator=(T *native) {
        (*_ptr) = native;
        return *this;
      }

      template<class S>
      self_type %operator=(T *native) {
        (*_ptr) = native;
        return *this;
      }

      static T *operator->(self_type %ap) { return ap._ptr->get(); }
      static T &operator*(self_type %ap) { return *(ap._ptr->get()); }
      T *get() { return _ptr->get(); }
      osg_ptr &osg() { return *_ptr; }

      void reset() {
        // _ptr is always allocated
        (*_ptr) = 0;
      }

    private:
      osg_ptr *_ptr;
    };
   
  }
}