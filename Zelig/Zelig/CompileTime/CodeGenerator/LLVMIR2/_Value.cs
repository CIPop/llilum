﻿using System;
using Llvm.NET.Values;

namespace Microsoft.Zelig.LLVM
{
    public class _Value
    {
        public _Type Type { get; }

        // in LLVM a module doesn't own a value, the context does
        // however, here a _Module is a container for the context,
        // a module and DiBuilder with assorted other state info.
        public _Module Module { get; }

        public bool IsInteger => LlvmValue.Type.IsInteger();

        public bool IsFloat => LlvmValue.Type.IsFloat( );

        public bool IsDouble => LlvmValue.Type.IsDouble( );

        public bool IsFloatingPoint => LlvmValue.Type.IsFloatingPoint( );

        public bool IsPointer => LlvmValue.Type.IsPointer();

        public bool IsPointerPointer => LlvmValue.Type.IsPointerPointer( );

        public bool IsImmediate { get; }

        public bool IsZeroedValue( )
        {
            var constant = LlvmValue as Constant;
            if( constant == null )
                return false;

            return constant.IsZeroValue;
        }

        public bool IsAnUninitializedGlobal( )
        {
            var gv = LlvmValue as GlobalVariable;
            if( gv == null )
                return false;

            return gv.Initializer == null;
        }

        public void SetGlobalInitializer( Constant val )
        {
            var gv = LlvmValue as GlobalVariable;
            if( gv != null )
            {
                gv.Initializer = val;
            }
        }

        public void MergeToAndRemove( _Value targetVal )
        {
            GlobalVariable gv = LlvmValue as GlobalVariable;
            if( gv != null )
            {
                gv.ReplaceAllUsesWith( targetVal.LlvmValue );
                gv.RemoveFromParent( );
            }
        }

        public void FlagAsConstant( )
        {
            var gv = LlvmValue as GlobalVariable;
            if( gv != null )
            {
                gv.IsConstant = true;
                gv.Section = ".text";
                gv.UnnamedAddress = true;
            }
        }

        internal _Value( _Module module, _Type type, Value value, bool isImmediate )
        {
            Module = module;
            Type = type;
            LlvmValue = value;
            IsImmediate = isImmediate;
        }

        internal Value LlvmValue { get; }
    }
}
