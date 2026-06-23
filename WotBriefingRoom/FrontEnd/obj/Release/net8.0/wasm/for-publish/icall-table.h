#define ICALL_TABLE_corlib 1

static int corlib_icall_indexes [] = {
181,
191,
192,
193,
194,
195,
196,
197,
198,
199,
202,
203,
285,
286,
287,
316,
317,
318,
338,
339,
340,
341,
433,
434,
435,
438,
475,
476,
478,
480,
482,
484,
489,
497,
498,
499,
500,
501,
502,
503,
504,
505,
599,
600,
666,
672,
675,
677,
682,
683,
685,
686,
690,
692,
693,
695,
697,
698,
701,
702,
703,
706,
708,
711,
713,
715,
724,
787,
789,
791,
801,
802,
803,
805,
811,
812,
813,
814,
815,
823,
824,
825,
829,
830,
832,
834,
1030,
1205,
1206,
7221,
7222,
7224,
7225,
7226,
7227,
7228,
7230,
7232,
7234,
7244,
7246,
7251,
7253,
7255,
7257,
7308,
7309,
7311,
7312,
7313,
7314,
7315,
7317,
7319,
8293,
8297,
8299,
8300,
8301,
8302,
8555,
8556,
8557,
8558,
8576,
8577,
8578,
8580,
8623,
8692,
8694,
8696,
8704,
8705,
8706,
8707,
9108,
9109,
9113,
9114,
9141,
9159,
9166,
9173,
9184,
9187,
9207,
9283,
9285,
9294,
9296,
9297,
9304,
9318,
9338,
9339,
9347,
9349,
9356,
9357,
9360,
9362,
9367,
9373,
9374,
9381,
9383,
9395,
9398,
9399,
9400,
9411,
9420,
9426,
9427,
9428,
9430,
9431,
9448,
9450,
9464,
9481,
9508,
9536,
9537,
10021,
10113,
10114,
10314,
10315,
10322,
10323,
10324,
10329,
10404,
10804,
10805,
11056,
11066,
11859,
11880,
11882,
11884,
};
void ves_icall_System_Array_InternalCreate (int,int,int,int,int);
int ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal (int);
int ves_icall_System_Array_IsValueOfElementTypeInternal (int,int);
int ves_icall_System_Array_CanChangePrimitive (int,int,int);
int ves_icall_System_Array_FastCopy (int,int,int,int,int);
int ves_icall_System_Array_GetLengthInternal_raw (int,int,int);
int ves_icall_System_Array_GetLowerBoundInternal_raw (int,int,int);
void ves_icall_System_Array_GetGenericValue_icall (int,int,int);
void ves_icall_System_Array_GetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetGenericValue_icall (int,int,int);
void ves_icall_System_Array_SetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetValueRelaxedImpl_raw (int,int,int,int);
void ves_icall_System_Runtime_RuntimeImports_ZeroMemory (int,int);
void ves_icall_System_Runtime_RuntimeImports_Memmove (int,int,int);
void ves_icall_System_Buffer_BulkMoveWithWriteBarrier (int,int,int,int);
int ves_icall_System_Delegate_AllocDelegateLike_internal_raw (int,int);
int ves_icall_System_Delegate_CreateDelegate_internal_raw (int,int,int,int,int);
int ves_icall_System_Delegate_GetVirtualMethod_internal_raw (int,int);
void ves_icall_System_Enum_GetEnumValuesAndNames_raw (int,int,int,int);
void ves_icall_System_Enum_InternalBoxEnum_raw (int,int,int64_t,int);
int ves_icall_System_Enum_InternalGetCorElementType (int);
void ves_icall_System_Enum_InternalGetUnderlyingType_raw (int,int,int);
int ves_icall_System_Environment_get_ProcessorCount ();
int ves_icall_System_Environment_get_TickCount ();
int64_t ves_icall_System_Environment_get_TickCount64 ();
void ves_icall_System_Environment_FailFast_raw (int,int,int,int);
void ves_icall_System_GC_register_ephemeron_array_raw (int,int);
int ves_icall_System_GC_get_ephemeron_tombstone_raw (int);
void ves_icall_System_GC_SuppressFinalize_raw (int,int);
void ves_icall_System_GC_ReRegisterForFinalize_raw (int,int);
void ves_icall_System_GC_GetGCMemoryInfo (int,int,int,int,int,int);
int ves_icall_System_GC_AllocPinnedArray_raw (int,int,int);
int ves_icall_System_Object_MemberwiseClone_raw (int,int);
double ves_icall_System_Math_Ceiling (double);
double ves_icall_System_Math_Cos (double);
double ves_icall_System_Math_Floor (double);
double ves_icall_System_Math_Log10 (double);
double ves_icall_System_Math_Pow (double,double);
double ves_icall_System_Math_Sin (double);
double ves_icall_System_Math_Sqrt (double);
double ves_icall_System_Math_Tan (double);
double ves_icall_System_Math_ModF (double,int);
void ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw (int,int,int);
void ves_icall_RuntimeMethodHandle_ReboxToNullable_raw (int,int,int,int);
int ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw (int,int,int);
void ves_icall_RuntimeType_make_array_type_raw (int,int,int,int);
void ves_icall_RuntimeType_make_byref_type_raw (int,int,int);
void ves_icall_RuntimeType_make_pointer_type_raw (int,int,int);
void ves_icall_RuntimeType_MakeGenericType_raw (int,int,int,int);
int ves_icall_RuntimeType_GetMethodsByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetPropertiesByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetConstructors_native_raw (int,int,int);
void ves_icall_RuntimeType_GetInterfaceMapData_raw (int,int,int,int,int);
int ves_icall_System_RuntimeType_CreateInstanceInternal_raw (int,int);
void ves_icall_System_RuntimeType_AllocateValueType_raw (int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringMethod_raw (int,int,int);
void ves_icall_System_RuntimeType_getFullName_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetGenericArgumentsInternal_raw (int,int,int,int);
int ves_icall_RuntimeType_GetGenericParameterPosition (int);
int ves_icall_RuntimeType_GetEvents_native_raw (int,int,int,int);
int ves_icall_RuntimeType_GetFields_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetInterfaces_raw (int,int,int);
int ves_icall_RuntimeType_GetNestedTypes_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringType_raw (int,int,int);
void ves_icall_RuntimeType_GetName_raw (int,int,int);
void ves_icall_RuntimeType_GetNamespace_raw (int,int,int);
int ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetAttributes (int);
int ves_icall_RuntimeTypeHandle_GetMetadataToken_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_GetCorElementType (int);
int ves_icall_RuntimeTypeHandle_HasInstantiation (int);
int ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_HasReferences_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetArrayRank_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetAssembly_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetElementType_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetModule_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetBaseType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition (int);
int ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw (int,int);
int ves_icall_RuntimeTypeHandle_is_subclass_of_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsByRefLike_raw (int,int);
void ves_icall_System_RuntimeTypeHandle_internal_from_name_raw (int,int,int,int,int,int);
int ves_icall_System_String_FastAllocateString_raw (int,int);
int ves_icall_System_Type_internal_from_handle_raw (int,int);
int ves_icall_System_ValueType_InternalGetHashCode_raw (int,int,int);
int ves_icall_System_ValueType_Equals_raw (int,int,int,int);
int ves_icall_System_Threading_Interlocked_CompareExchange_Int (int,int,int);
void ves_icall_System_Threading_Interlocked_CompareExchange_Object (int,int,int,int);
int ves_icall_System_Threading_Interlocked_Decrement_Int (int);
int ves_icall_System_Threading_Interlocked_Increment_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Increment_Long (int);
int ves_icall_System_Threading_Interlocked_Exchange_Int (int,int);
void ves_icall_System_Threading_Interlocked_Exchange_Object (int,int,int);
int64_t ves_icall_System_Threading_Interlocked_CompareExchange_Long (int,int64_t,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Exchange_Long (int,int64_t);
int ves_icall_System_Threading_Interlocked_Add_Int (int,int);
void ves_icall_System_Threading_Monitor_Monitor_Enter_raw (int,int);
void mono_monitor_exit_icall_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw (int,int);
int ves_icall_System_Threading_Monitor_Monitor_wait_raw (int,int,int,int);
void ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw (int,int,int,int,int);
void ves_icall_System_Threading_Thread_InitInternal_raw (int,int);
int ves_icall_System_Threading_Thread_GetCurrentThread ();
void ves_icall_System_Threading_InternalThread_Thread_free_internal_raw (int,int);
int ves_icall_System_Threading_Thread_GetState_raw (int,int);
void ves_icall_System_Threading_Thread_SetState_raw (int,int,int);
void ves_icall_System_Threading_Thread_ClrState_raw (int,int,int);
void ves_icall_System_Threading_Thread_SetName_icall_raw (int,int,int,int);
int ves_icall_System_Threading_Thread_YieldInternal ();
void ves_icall_System_Threading_Thread_SetPriority_raw (int,int,int);
void ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw (int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw (int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw (int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw (int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw (int);
int ves_icall_System_GCHandle_InternalAlloc_raw (int,int,int);
void ves_icall_System_GCHandle_InternalFree_raw (int,int);
int ves_icall_System_GCHandle_InternalGet_raw (int,int);
void ves_icall_System_GCHandle_InternalSet_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError ();
void ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError (int);
void ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw (int,int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalTryGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw (int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw (int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw (int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack ();
int ves_icall_System_Reflection_Assembly_GetCallingAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_InternalLoad_raw (int,int,int,int);
int ves_icall_System_Reflection_Assembly_InternalGetType_raw (int,int,int,int,int,int);
int ves_icall_System_Reflection_AssemblyName_GetNativeName (int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw (int,int,int,int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw (int,int);
int ves_icall_MonoCustomAttrs_IsDefinedInternal_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw (int,int);
int ves_icall_System_Reflection_LoaderAllocatorScout_Destroy (int);
void ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw (int,int,int,int);
int ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw (int,int,int,int,int);
void ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw (int,int,int,int,int,int,int);
void ves_icall_RuntimeEventInfo_get_event_info_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_ResolveType_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetParentType_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_GetFieldOffset_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetValueInternal_raw (int,int,int);
void ves_icall_RuntimeFieldInfo_SetValueInternal_raw (int,int,int,int);
int ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw (int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_get_method_info_raw (int,int,int);
int ves_icall_get_method_attributes (int);
int ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw (int,int,int);
int ves_icall_System_MonoMethodInfo_get_retval_marshal_raw (int,int);
int ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw (int,int,int,int);
int ves_icall_RuntimeMethodInfo_get_name_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_base_method_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
void ves_icall_RuntimeMethodInfo_GetPInvoke_raw (int,int,int,int,int);
int ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw (int,int,int);
int ves_icall_RuntimeMethodInfo_GetGenericArguments_raw (int,int);
int ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw (int,int);
void ves_icall_InvokeClassConstructor_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw (int,int,int,int,int,int);
void ves_icall_RuntimePropertyInfo_get_property_info_raw (int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw (int,int,int);
void ves_icall_DynamicMethod_create_dynamic_method_raw (int,int,int,int,int);
void ves_icall_AssemblyBuilder_basic_init_raw (int,int);
void ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw (int,int);
void ves_icall_ModuleBuilder_basic_init_raw (int,int);
void ves_icall_ModuleBuilder_set_wrappers_type_raw (int,int,int);
int ves_icall_ModuleBuilder_getUSIndex_raw (int,int,int);
int ves_icall_ModuleBuilder_getToken_raw (int,int,int,int);
int ves_icall_ModuleBuilder_getMethodToken_raw (int,int,int,int);
void ves_icall_ModuleBuilder_RegisterToken_raw (int,int,int,int);
int ves_icall_TypeBuilder_create_runtime_class_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw (int,int);
int ves_icall_System_Diagnostics_StackFrame_GetFrameInfo (int,int,int,int,int,int,int,int);
void ves_icall_System_Diagnostics_StackTrace_GetTrace (int,int,int,int);
int ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass (int);
void ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree (int);
int ves_icall_Mono_SafeStringMarshal_StringToUtf8 (int);
void ves_icall_Mono_SafeStringMarshal_GFree (int);
static void *corlib_icall_funcs [] = {
// token 181,
ves_icall_System_Array_InternalCreate,
// token 191,
ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal,
// token 192,
ves_icall_System_Array_IsValueOfElementTypeInternal,
// token 193,
ves_icall_System_Array_CanChangePrimitive,
// token 194,
ves_icall_System_Array_FastCopy,
// token 195,
ves_icall_System_Array_GetLengthInternal_raw,
// token 196,
ves_icall_System_Array_GetLowerBoundInternal_raw,
// token 197,
ves_icall_System_Array_GetGenericValue_icall,
// token 198,
ves_icall_System_Array_GetValueImpl_raw,
// token 199,
ves_icall_System_Array_SetGenericValue_icall,
// token 202,
ves_icall_System_Array_SetValueImpl_raw,
// token 203,
ves_icall_System_Array_SetValueRelaxedImpl_raw,
// token 285,
ves_icall_System_Runtime_RuntimeImports_ZeroMemory,
// token 286,
ves_icall_System_Runtime_RuntimeImports_Memmove,
// token 287,
ves_icall_System_Buffer_BulkMoveWithWriteBarrier,
// token 316,
ves_icall_System_Delegate_AllocDelegateLike_internal_raw,
// token 317,
ves_icall_System_Delegate_CreateDelegate_internal_raw,
// token 318,
ves_icall_System_Delegate_GetVirtualMethod_internal_raw,
// token 338,
ves_icall_System_Enum_GetEnumValuesAndNames_raw,
// token 339,
ves_icall_System_Enum_InternalBoxEnum_raw,
// token 340,
ves_icall_System_Enum_InternalGetCorElementType,
// token 341,
ves_icall_System_Enum_InternalGetUnderlyingType_raw,
// token 433,
ves_icall_System_Environment_get_ProcessorCount,
// token 434,
ves_icall_System_Environment_get_TickCount,
// token 435,
ves_icall_System_Environment_get_TickCount64,
// token 438,
ves_icall_System_Environment_FailFast_raw,
// token 475,
ves_icall_System_GC_register_ephemeron_array_raw,
// token 476,
ves_icall_System_GC_get_ephemeron_tombstone_raw,
// token 478,
ves_icall_System_GC_SuppressFinalize_raw,
// token 480,
ves_icall_System_GC_ReRegisterForFinalize_raw,
// token 482,
ves_icall_System_GC_GetGCMemoryInfo,
// token 484,
ves_icall_System_GC_AllocPinnedArray_raw,
// token 489,
ves_icall_System_Object_MemberwiseClone_raw,
// token 497,
ves_icall_System_Math_Ceiling,
// token 498,
ves_icall_System_Math_Cos,
// token 499,
ves_icall_System_Math_Floor,
// token 500,
ves_icall_System_Math_Log10,
// token 501,
ves_icall_System_Math_Pow,
// token 502,
ves_icall_System_Math_Sin,
// token 503,
ves_icall_System_Math_Sqrt,
// token 504,
ves_icall_System_Math_Tan,
// token 505,
ves_icall_System_Math_ModF,
// token 599,
ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw,
// token 600,
ves_icall_RuntimeMethodHandle_ReboxToNullable_raw,
// token 666,
ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw,
// token 672,
ves_icall_RuntimeType_make_array_type_raw,
// token 675,
ves_icall_RuntimeType_make_byref_type_raw,
// token 677,
ves_icall_RuntimeType_make_pointer_type_raw,
// token 682,
ves_icall_RuntimeType_MakeGenericType_raw,
// token 683,
ves_icall_RuntimeType_GetMethodsByName_native_raw,
// token 685,
ves_icall_RuntimeType_GetPropertiesByName_native_raw,
// token 686,
ves_icall_RuntimeType_GetConstructors_native_raw,
// token 690,
ves_icall_RuntimeType_GetInterfaceMapData_raw,
// token 692,
ves_icall_System_RuntimeType_CreateInstanceInternal_raw,
// token 693,
ves_icall_System_RuntimeType_AllocateValueType_raw,
// token 695,
ves_icall_RuntimeType_GetDeclaringMethod_raw,
// token 697,
ves_icall_System_RuntimeType_getFullName_raw,
// token 698,
ves_icall_RuntimeType_GetGenericArgumentsInternal_raw,
// token 701,
ves_icall_RuntimeType_GetGenericParameterPosition,
// token 702,
ves_icall_RuntimeType_GetEvents_native_raw,
// token 703,
ves_icall_RuntimeType_GetFields_native_raw,
// token 706,
ves_icall_RuntimeType_GetInterfaces_raw,
// token 708,
ves_icall_RuntimeType_GetNestedTypes_native_raw,
// token 711,
ves_icall_RuntimeType_GetDeclaringType_raw,
// token 713,
ves_icall_RuntimeType_GetName_raw,
// token 715,
ves_icall_RuntimeType_GetNamespace_raw,
// token 724,
ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw,
// token 787,
ves_icall_RuntimeTypeHandle_GetAttributes,
// token 789,
ves_icall_RuntimeTypeHandle_GetMetadataToken_raw,
// token 791,
ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw,
// token 801,
ves_icall_RuntimeTypeHandle_GetCorElementType,
// token 802,
ves_icall_RuntimeTypeHandle_HasInstantiation,
// token 803,
ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw,
// token 805,
ves_icall_RuntimeTypeHandle_HasReferences_raw,
// token 811,
ves_icall_RuntimeTypeHandle_GetArrayRank_raw,
// token 812,
ves_icall_RuntimeTypeHandle_GetAssembly_raw,
// token 813,
ves_icall_RuntimeTypeHandle_GetElementType_raw,
// token 814,
ves_icall_RuntimeTypeHandle_GetModule_raw,
// token 815,
ves_icall_RuntimeTypeHandle_GetBaseType_raw,
// token 823,
ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw,
// token 824,
ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition,
// token 825,
ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw,
// token 829,
ves_icall_RuntimeTypeHandle_is_subclass_of_raw,
// token 830,
ves_icall_RuntimeTypeHandle_IsByRefLike_raw,
// token 832,
ves_icall_System_RuntimeTypeHandle_internal_from_name_raw,
// token 834,
ves_icall_System_String_FastAllocateString_raw,
// token 1030,
ves_icall_System_Type_internal_from_handle_raw,
// token 1205,
ves_icall_System_ValueType_InternalGetHashCode_raw,
// token 1206,
ves_icall_System_ValueType_Equals_raw,
// token 7221,
ves_icall_System_Threading_Interlocked_CompareExchange_Int,
// token 7222,
ves_icall_System_Threading_Interlocked_CompareExchange_Object,
// token 7224,
ves_icall_System_Threading_Interlocked_Decrement_Int,
// token 7225,
ves_icall_System_Threading_Interlocked_Increment_Int,
// token 7226,
ves_icall_System_Threading_Interlocked_Increment_Long,
// token 7227,
ves_icall_System_Threading_Interlocked_Exchange_Int,
// token 7228,
ves_icall_System_Threading_Interlocked_Exchange_Object,
// token 7230,
ves_icall_System_Threading_Interlocked_CompareExchange_Long,
// token 7232,
ves_icall_System_Threading_Interlocked_Exchange_Long,
// token 7234,
ves_icall_System_Threading_Interlocked_Add_Int,
// token 7244,
ves_icall_System_Threading_Monitor_Monitor_Enter_raw,
// token 7246,
mono_monitor_exit_icall_raw,
// token 7251,
ves_icall_System_Threading_Monitor_Monitor_pulse_raw,
// token 7253,
ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw,
// token 7255,
ves_icall_System_Threading_Monitor_Monitor_wait_raw,
// token 7257,
ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw,
// token 7308,
ves_icall_System_Threading_Thread_InitInternal_raw,
// token 7309,
ves_icall_System_Threading_Thread_GetCurrentThread,
// token 7311,
ves_icall_System_Threading_InternalThread_Thread_free_internal_raw,
// token 7312,
ves_icall_System_Threading_Thread_GetState_raw,
// token 7313,
ves_icall_System_Threading_Thread_SetState_raw,
// token 7314,
ves_icall_System_Threading_Thread_ClrState_raw,
// token 7315,
ves_icall_System_Threading_Thread_SetName_icall_raw,
// token 7317,
ves_icall_System_Threading_Thread_YieldInternal,
// token 7319,
ves_icall_System_Threading_Thread_SetPriority_raw,
// token 8293,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw,
// token 8297,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw,
// token 8299,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw,
// token 8300,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw,
// token 8301,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw,
// token 8302,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw,
// token 8555,
ves_icall_System_GCHandle_InternalAlloc_raw,
// token 8556,
ves_icall_System_GCHandle_InternalFree_raw,
// token 8557,
ves_icall_System_GCHandle_InternalGet_raw,
// token 8558,
ves_icall_System_GCHandle_InternalSet_raw,
// token 8576,
ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError,
// token 8577,
ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError,
// token 8578,
ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw,
// token 8580,
ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw,
// token 8623,
ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw,
// token 8692,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw,
// token 8694,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalTryGetHashCode_raw,
// token 8696,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw,
// token 8704,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw,
// token 8705,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw,
// token 8706,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw,
// token 8707,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack,
// token 9108,
ves_icall_System_Reflection_Assembly_GetCallingAssembly_raw,
// token 9109,
ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw,
// token 9113,
ves_icall_System_Reflection_Assembly_InternalLoad_raw,
// token 9114,
ves_icall_System_Reflection_Assembly_InternalGetType_raw,
// token 9141,
ves_icall_System_Reflection_AssemblyName_GetNativeName,
// token 9159,
ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw,
// token 9166,
ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw,
// token 9173,
ves_icall_MonoCustomAttrs_IsDefinedInternal_raw,
// token 9184,
ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw,
// token 9187,
ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw,
// token 9207,
ves_icall_System_Reflection_LoaderAllocatorScout_Destroy,
// token 9283,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw,
// token 9285,
ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw,
// token 9294,
ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw,
// token 9296,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw,
// token 9297,
ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw,
// token 9304,
ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw,
// token 9318,
ves_icall_RuntimeEventInfo_get_event_info_raw,
// token 9338,
ves_icall_reflection_get_token_raw,
// token 9339,
ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw,
// token 9347,
ves_icall_RuntimeFieldInfo_ResolveType_raw,
// token 9349,
ves_icall_RuntimeFieldInfo_GetParentType_raw,
// token 9356,
ves_icall_RuntimeFieldInfo_GetFieldOffset_raw,
// token 9357,
ves_icall_RuntimeFieldInfo_GetValueInternal_raw,
// token 9360,
ves_icall_RuntimeFieldInfo_SetValueInternal_raw,
// token 9362,
ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw,
// token 9367,
ves_icall_reflection_get_token_raw,
// token 9373,
ves_icall_get_method_info_raw,
// token 9374,
ves_icall_get_method_attributes,
// token 9381,
ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw,
// token 9383,
ves_icall_System_MonoMethodInfo_get_retval_marshal_raw,
// token 9395,
ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw,
// token 9398,
ves_icall_RuntimeMethodInfo_get_name_raw,
// token 9399,
ves_icall_RuntimeMethodInfo_get_base_method_raw,
// token 9400,
ves_icall_reflection_get_token_raw,
// token 9411,
ves_icall_InternalInvoke_raw,
// token 9420,
ves_icall_RuntimeMethodInfo_GetPInvoke_raw,
// token 9426,
ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw,
// token 9427,
ves_icall_RuntimeMethodInfo_GetGenericArguments_raw,
// token 9428,
ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw,
// token 9430,
ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw,
// token 9431,
ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw,
// token 9448,
ves_icall_InvokeClassConstructor_raw,
// token 9450,
ves_icall_InternalInvoke_raw,
// token 9464,
ves_icall_reflection_get_token_raw,
// token 9481,
ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw,
// token 9508,
ves_icall_RuntimePropertyInfo_get_property_info_raw,
// token 9536,
ves_icall_reflection_get_token_raw,
// token 9537,
ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw,
// token 10021,
ves_icall_DynamicMethod_create_dynamic_method_raw,
// token 10113,
ves_icall_AssemblyBuilder_basic_init_raw,
// token 10114,
ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw,
// token 10314,
ves_icall_ModuleBuilder_basic_init_raw,
// token 10315,
ves_icall_ModuleBuilder_set_wrappers_type_raw,
// token 10322,
ves_icall_ModuleBuilder_getUSIndex_raw,
// token 10323,
ves_icall_ModuleBuilder_getToken_raw,
// token 10324,
ves_icall_ModuleBuilder_getMethodToken_raw,
// token 10329,
ves_icall_ModuleBuilder_RegisterToken_raw,
// token 10404,
ves_icall_TypeBuilder_create_runtime_class_raw,
// token 10804,
ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw,
// token 10805,
ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw,
// token 11056,
ves_icall_System_Diagnostics_StackFrame_GetFrameInfo,
// token 11066,
ves_icall_System_Diagnostics_StackTrace_GetTrace,
// token 11859,
ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass,
// token 11880,
ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree,
// token 11882,
ves_icall_Mono_SafeStringMarshal_StringToUtf8,
// token 11884,
ves_icall_Mono_SafeStringMarshal_GFree,
};
static uint8_t corlib_icall_flags [] = {
0,
0,
0,
0,
0,
4,
4,
0,
4,
0,
4,
4,
0,
0,
0,
4,
4,
4,
4,
4,
0,
4,
0,
0,
0,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
};
