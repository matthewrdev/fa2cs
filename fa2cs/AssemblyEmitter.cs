using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace fa2cs
{
    /// <summary>
    /// See: https://josephwoodward.co.uk/2016/12/in-memory-c-sharp-compilation-using-roslyn
    /// </summary>
    public static class AssemblyEmitter
    {
        private static readonly IReadOnlyCollection<MetadataReference> _references = new[] {
          MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location),
          MetadataReference.CreateFromFile(typeof(ValueTuple<>).GetTypeInfo().Assembly.Location)
      };

        public static bool EmitAssembly(List<string> sources, 
                                        string outputPath)
        {
            List<SyntaxTree> syntaxTrees = sources.Select(code => CSharpSyntaxTree.ParseText(code, CSharpParseOptions.Default)).ToList();

            Compilation compilation = CreateLibraryCompilation("FontAwesome.IconCodes")
                                      .AddReferences(_references)
                                       .AddSyntaxTrees(syntaxTrees);

            var assemblyStream = new MemoryStream();
            var docsStream = new MemoryStream();
            var emitResult = compilation.Emit(assemblyStream, xmlDocumentationStream:docsStream);

            var assemblyPath = Path.Combine(outputPath, "FontAwesome.IconCodes.dll");
            var docsPath = Path.Combine(outputPath, "FontAwesome.IconCodes.xml");

            if (emitResult.Success)
            {
                assemblyStream.Seek(0, SeekOrigin.Begin);
                docsStream.Seek(0, SeekOrigin.Begin);

                using (FileStream fs = new FileStream(assemblyPath, FileMode.OpenOrCreate))
                {
                    assemblyStream.CopyTo(fs);
                    fs.Flush();
                }

                using (FileStream fs = new FileStream(docsPath, FileMode.OpenOrCreate))
                {
                    docsStream.CopyTo(fs);
                    fs.Flush();
                }

                return true;
            }

            return false;
        }

        static Compilation CreateLibraryCompilation(string assemblyName)
        {
            var options = new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                optimizationLevel: OptimizationLevel.Release);

            return CSharpCompilation.Create(assemblyName, 
                                            options: options, 
                                            references: _references);
        }
    }
}
