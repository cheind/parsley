/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

#pragma once
#include <string>

namespace Parsley {
  namespace Draw3D {    
    namespace {
      void marshal( System::String ^ s, std::string& os ) {
         using namespace System::Runtime::InteropServices;
         const char* chars = 
            (const char*)(Marshal::StringToHGlobalAnsi(s)).ToPointer();
         os = chars;
         Marshal::FreeHGlobal(System::IntPtr((void*)chars));
      }

      void marshal ( System::String ^ s, std::wstring& os ) {
         using namespace System::Runtime::InteropServices;
         const wchar_t* chars = 
            (const wchar_t*)(Marshal::StringToHGlobalUni(s)).ToPointer();
         os = chars;
         Marshal::FreeHGlobal(System::IntPtr((void*)chars));
      }
    }
  }
}