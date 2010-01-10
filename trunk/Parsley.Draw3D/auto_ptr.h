#pragma once

#include <osg/ref_ptr>

namespace Parsley {
  namespace Draw3D {

    /// CLI auto_ptr class to native type
    /// http://www.sgi.com/tech/stl/memory
    template<class T>
    public ref class auto_ptr {
    public:
      typedef T value_type;

      auto_ptr() : _ptr(0) {}
      explicit auto_ptr(T *ptr) : _ptr(ptr) {}
      auto_ptr(auto_ptr<T> %aptr) : _ptr(a.release()) {}
      template<class S>
      auto_ptr(auto_ptr<S> %aptr) : _ptr(a.release()) {}
      ~auto_ptr() { this->reset(); }
      !auto_ptr() { this->reset(); }
      
      // Assignment
      auto_ptr<T> %operator=(auto_ptr<T> %rhs) {
        if (&rhs != this) {
          this->reset();
          _ptr = rhs.release();
        }
        return *this;
      }

      template<class S>
      auto_ptr<T> %operator=(auto_ptr<S> %rhs) {
        if (&rhs != this) {
          this->reset();
          _ptr = rhs.release();
        }
        return *this;
      }
      
      static T *operator->(auto_ptr<T> %ap) { return ap._ptr; }
      static T &operator*(auto_ptr<T> %ap) { return *ap._ptr; }
      T *get() { return _ptr; }

      T *release() {
        _T *tmp = _ptr;
        _ptr = 0;
        return tmp;
      }

      void reset() {
        if (_ptr != 0) {
          delete _ptr;
          _ptr = 0;
        }
      }

    private:
      T *_ptr;
    };
  }
}