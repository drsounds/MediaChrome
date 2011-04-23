using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Board
{
    /// <summary>
    /// Interface for dealing with various script engines to connect. All engines must be wrapped by this interface.
    /// </summary>

    public interface IScriptEngine
    {
        
        /// <summary>
        /// This function is called by the preprocesor after the code has been compiled from the preprocessing template
        /// </summary>
        /// <param name="scriptCode">The string with the javascript code</param>
        /// <returns>True if sucess, false if failed </returns>
        bool Run(string scriptCode);

        /// <summary>
        /// Thus method is called to make an instance of an certain object accessible for the scripting object.
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="varInstance"></param>
        void SetVariable(string variableName, object varInstance);

        /// <summary>
        /// This method is called to set an function to be accessible by the functionName for the script once Executed.
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="functionPointer"></param>
        void SetFunction(string functionName, Delegate functionPointer);
    }

    public class JavaScriptEngine : IScriptEngine
    {
        /// <summary>
        /// The instance of the Jint parser
        /// </summary>
        Jint.JintEngine scriptEngine;

        public JavaScriptEngine()
        {
            scriptEngine = new Jint.JintEngine();
            scriptEngine.AllowClr = true;
            scriptEngine.DisableSecurity();
        }

        /// <summary>
        /// This function is called by the preprocesor after the code has been compiled from the preprocessing template
        /// </summary>
        /// <param name="scriptCode">The string with the javascript code</param>
        /// <returns>True if sucess, false if failed </returns>
        public bool  Run(string scriptCode)
        {
            try
            {
                scriptEngine.Run(scriptCode);
                return true;
            }

            catch
            {
                return false;
            }
        }
    

        /// <summary>
        /// Thus method is called to make an instance of an certain object accessible for the scripting object.
        /// </summary>
        /// <param name="variableName">The name of the instance at the script level</param>
        /// <param name="varInstance">The object to send</param>
        public void SetVariable(string variableName, object varInstance)
        {
            scriptEngine.SetParameter(variableName, varInstance);
        }

        /// <summary>
        /// This method is called to set an function to be accessible by the functionName for the script once Executed.
        /// </summary>
        /// <param name="functionName">The desired alias for the function at script level</param>
        /// <param name="functionPointer">The delegate for the function to use at script</param>
        public void SetFunction(string functionName, Delegate functionPointer)
        {
            scriptEngine.SetFunction(functionName, functionPointer);
        }
    }
}
