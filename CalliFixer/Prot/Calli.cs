﻿using CalliFixer;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalliFixer.Protections
{
    public class Calli
    {

        public static void run(ModuleDefMD module)
        {
            int decrypted = 0;
            foreach (var type in module.GetTypes())
                foreach (var method in type.Methods)
                {
                    if (!method.HasBody)
                        continue;
                    var instructions = method.Body.Instructions;
                    for (int i = 2; i < instructions.Count; i++)
                    {

                        if (instructions[i].OpCode == OpCodes.Ldftn)
                        {
                            instructions[i].OpCode = OpCodes.Callvirt;
                           

                            decrypted++;
                        }
                        if (instructions[i].OpCode == OpCodes.Calli)
                        {
                            instructions[i].OpCode = OpCodes.Nop;
                            decrypted++; 
                        }


                    }
                
               }
            Logger.Write($"Removed and Decrypted {decrypted} calli calls.", Logger.Type.Info);
        }
    }
}
