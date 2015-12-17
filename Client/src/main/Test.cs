using System;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Test {
    public class Program {
        static void Main4(string[] args) {

            AssemblyDefinition asm = AssemblyDefinition.ReadAssembly(@"UnityEngine.dll");
            TypeDefinition type = asm.MainModule.GetType("UnityEngine", "Input");

            TypeDefinition typeVec3 = asm.MainModule.GetType("UnityEngine.Vector3");
            TypeReference typeRefVec3 = typeVec3.GetElementType();
            FieldDefinition simMouseVec = new FieldDefinition("s_simMouseVec", FieldAttributes.Public | FieldAttributes.Static, typeRefVec3);
            type.Fields.Add(simMouseVec);
            FieldReference simMouseVecRef = new FieldReference("s_simMouseVec", typeRefVec3, type);

            TypeReference boolType = asm.MainModule.ImportReference(typeof(bool));
            FieldDefinition useSim = new FieldDefinition("m_simActive", FieldAttributes.Public | FieldAttributes.Static, boolType);
            type.Fields.Add(useSim);
            FieldReference useSimRef = new FieldReference("m_simActive", boolType, type);

            MethodReference vec3CnstrRef = null;
            foreach (var m in typeVec3.Methods) {
                if (m.FullName == "System.Void UnityEngine.Vector3::.ctor(System.Single,System.Single,System.Single)") {
                    vec3CnstrRef = m.GetElementMethod();
                    break;
                }
            }

            asm.MainModule.ImportReference(simMouseVecRef);

            foreach (MethodDefinition method in type.Methods) {

                if (method.Name == ".cctor") {
                    var proc = method.Body.GetILProcessor();

                    Instruction i0 = proc.Create(OpCodes.Ldc_R4, (float)500);
                    Instruction i1 = proc.Create(OpCodes.Ldc_R4, (float)500);
                    Instruction i2 = proc.Create(OpCodes.Ldc_R4, (float)0);
                    Instruction i3 = proc.Create(OpCodes.Newobj, vec3CnstrRef);
                    Instruction i4 = proc.Create(OpCodes.Stsfld, simMouseVecRef);

                    proc.InsertBefore(method.Body.Instructions[0], i4);
                    proc.InsertBefore(i4, i3);
                    proc.InsertBefore(i3, i2);
                    proc.InsertBefore(i2, i1);
                    proc.InsertBefore(i1, i0);
                }
                if (method.Name == "get_mousePosition") {
                    var proc = method.Body.GetILProcessor();

                    Instruction ldsfld = proc.Create(OpCodes.Ldsfld, simMouseVecRef);
                    Instruction ret = proc.Create(OpCodes.Ret);
                    Instruction ldsfldFlag = proc.Create(OpCodes.Ldsfld, useSimRef);
                    Instruction brfalses = proc.Create(OpCodes.Brfalse_S, method.Body.Instructions[0]);

                    proc.InsertBefore(method.Body.Instructions[0], ret);
                    proc.InsertBefore(ret, ldsfld);
                    proc.InsertBefore(ldsfld, brfalses);
                    proc.InsertBefore(brfalses, ldsfldFlag);
                }
            }


            asm.Write(@"../../../Client/bin/Debug/UnityEngineExt.dll");

            //Console.Read();

        }

        static void Main3(string[] args) {

            var asm = AssemblyDefinition.ReadAssembly(@"Assembly-CSharp.dll");
            var type = asm.MainModule.GetType("GameState");

            foreach (MethodDefinition method in type.Methods) {
                if (method.Name == "OnTagChange") {
                    Console.WriteLine(method.Name);
                    method.Attributes = MethodAttributes.Virtual | MethodAttributes.Public;
                    break;
                }
            }

            asm.Write(@"Assembly-CSharp-Ext.dll");

            Console.Read();

        }
    }
}
