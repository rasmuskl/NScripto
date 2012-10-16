using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace NScripto.Documentation
{
    public interface IScriptDocumentationService
    {
        ScriptDocumentation GetDocumentation(Assembly assembly);
    }
}