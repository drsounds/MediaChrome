using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jint;
using System.Net;
using System.Xml;
namespace Board
{
    /// <summary>
    /// This is an simple MakoEngine implementation for Jint.
    /// Used to minic the Spotify's view engine.
    /// 
    /// </summary>
    public class MakoEngine
    {
        public MakoEngine()
        {
            // Initialize runtime engine. For now we use JavaScriptEngine
            RuntimeMachine = new JavaScriptEngine();
        }
        String Output = "";
        /// <summary>
        /// Callback where the output is thrown to, called by the parsed string
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public object __printx( string values)
        {
            Output += values.Replace("%BR%","\n").Replace("¤","\"");
            return true;
        }
        /// <summary>
        /// Synchronize data is called by the javascript preparser to get an ready to use JSON parsed data. If the dat can't be parsed as JSON
        /// it will be returned as an common string
        /// </summary>
        /// <param name="uri">The address to the remote information to retrieve</param>
        /// <returns></returns>
        public object synchronize_data(string uri)
        {
            // Create web request
            WebClient WC = new WebClient();
            /**
             * Try getting data. If no data was got an all, return FALSE
             * */
            try
            {
                String jsonRaw = WC.DownloadString(new Uri(uri));

                // Convert it to JSON
                try
                {
                    Jint.JintEngine Engine = new JintEngine();
                    Jint.Native.JsObject D = new Jint.Native.JsObject((object)jsonRaw);

                    // Try parse it as json, otherwise try as xml and if not retuurn it as an string
                    try
                    {
                        // Do not allow CLR when reading external scripts for security measurements
                        System.Web.Script.Serialization.JavaScriptSerializer d = new System.Web.Script.Serialization.JavaScriptSerializer();
                        object json = d.DeserializeObject(jsonRaw);
                        return json;
                    }
                    catch
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(jsonRaw);
                        return xmlDoc;
                    }

                   
                }
                catch
                {
                    return jsonRaw;
                }
            }
            catch
            {

                return false;
            }
        }

        /// <summary>
        /// Function to convert variable signatures to variable concations for the parser
        /// </summary>
        /// <param name="Line"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public string HandleToTokens(String Line, char signature)
        {
            
            Dictionary<String, Object> Variables = new Dictionary<String, Object>();
            // The index of the beginning of an varialbe statement @{
            int indexOf = 0;
            /**
             * Iterate through all indexes of the @{ statemenet until it ends (indexOf will return -1  becase indexOf will gain
             * the new indexOf with the new statement
             * */
            if(Line.Length > 0)
            while (indexOf != -1)
            {
                indexOf = Line.IndexOf(signature + "{", indexOf);
                if (indexOf == -1)
                    break;
                // Gain the index of the next occuranse of the @{ varialbe
                
                int endToken = Line.IndexOf('}', indexOf);

                int startIndex = indexOf + 2;

                // Get the data inside the token
                String Parseable = Line.Substring(startIndex,  endToken -  startIndex);

                // Convert the inline token to concation
                Line = Line.Replace("@{" + Parseable + "}",  "\" + ( "  + Parseable + " ) + \"");
                indexOf = endToken;
               
            }
            return Line;
         

        }
        /// <summary>
        /// This function returns variable from the parser embedded in an output field, asserted with an custom sign {VARNAME}
        /// </summary>
        /// <param name="Line">The code line to execute</param>
        /// <param name="signature">The char signature</param>
        /// <returns>An list of processed variables</returns>
        public Dictionary<String, Object> GetVariables(String Line, char signature)
        {
            Dictionary<String,Object> Variables = new Dictionary<String,Object>();
            // The index of the beginning of an varialbe statement @{
            int indexOf = 0;
            /**
             * Iterate through all indexes of the @{ statemenet until it ends (indexOf will return -1  becase indexOf will gain
             * the new indexOf with the new statement
             * */
            while (indexOf != -1)
            {
                // Gain the index of the next occuranse of the @{ varialbe
                indexOf  = Line.IndexOf(signature+"{");
                int endToken = Line.IndexOf("}", indexOf);

                int startIndex = indexOf+2;

                // Get the data inside the token
                String Parseable = Line.Substring(startIndex, startIndex + endToken - 1);

                // Convert it into an variable
                String[] Result = ExecuteScalarVariable(Parseable, ':', '|', true);
                Variables.Add(Result[0], Result[1]);
            }
            return Variables;
           
        }
        /// <summary>
        /// This function executes the scalar variable, works together with GetVariable. It will also parse the inherited 
        /// codebase.
        /// </summary>
        /// <param name="Variable"></param>
        /// <param name="reflector">Reflector divides which is the conditinoal statement and the boolean output</param>
        /// <param name="divider">Boolean divider</param>
        /// <param name="vetero">Which variable beside the reflector divider should be present in fallback</param>
        /// <value>Returns an 2 field String array where {InitialVariableName,Output}</value>
        /// <returns></returns>
        public String[] ExecuteScalarVariable(String Variable,char reflector,char divider,bool vetero)
        {
            // An variable in this instruction {boolVar} : return1 | return 2
            if (Variable.Contains(reflector) && Variable.Contains(divider))
            {
                // Get the codebase
                String Codebase = Variable.Split(reflector)[0];

                // Run the codebase
                object d = RuntimeMachine.Run(Codebase);

                // If it are an boolean decide it, otherwise return the left/right variable as fallback decided by the vetero varialbe
                if (d.GetType() == typeof(bool))
                {
                    // Get the two case output
                    String[] c = Variable.Split(reflector)[1].Split(divider);

                    // Return the decition
                    String output =  (bool)d ? c[0] : c[1];
                    return new String[] { Codebase, output };
                }
                else
                {
                    String[] c = Variable.Split(reflector)[1].Split(divider);

                    // Return the case fallback
                    String output = vetero ? c[0] : c[1];
                    return new String[] { Codebase, output };
                }
            }
           
            /**
                * Otherwise return the value of the variable asserted by the current state of the execution instance
                * */
          
            // Output data
            object _output = RuntimeMachine.Run("return " + Variable + ";");
            if (_output.GetType() == typeof(String))
            {
                return new String[]{Variable,(String)_output};
            }

            return new String[]{Variable,Variable};
           
            
        }
        /// <summary>
        /// Instance of the Jint engine running at runtime
        /// </summary>
        public IScriptEngine RuntimeMachine { get; set; }
        /// <summary>
        /// This function executes string in the js mako engine
        /// </summary>
        /// <param name="e"></param>
        public void Execute(string e)
        {
           
            RuntimeMachine.Run(e);
        }
        /// <summary>
        /// This function preprosses the mako layer
        /// </summary>
        /// <param name="input">The input string to parse</param>
        /// <param name="argument">The argument sent to the parser</param>
        public String Preprocess(string input,string argument)
        {
            /***
             * Tell the runtime machine about the argument
             * */
            RuntimeMachine.SetVariable("parameter", argument);
            /**
             * This string defines the call-stack of the query
             * This is done before any other preprocessing
             * */
            String CallStack = "";
            String[] lines = input.Split('\n');
            /**
             * Boolean indicating which parse mode the parser is in,
             * true = in executable text
             * false = in output line 
             * */
            bool parseMode = false;
            // Boolean indicating first line is parsing
            bool firstLine = true;
            /***
             * Iterate through all lines and preprocess the page.
             * If page starts with an % it will be treated as an preparser code or all content
             * inside enclosing <? ?>
             * Two string builders, the first is for the current output segment and the second for the current
             * executable segmetn
             * */
            StringBuilder outputCode =  new StringBuilder();
            StringBuilder executableSegment = new StringBuilder();

            // The buffer for the final preprocessing output
            StringBuilder finalOutput = new StringBuilder();
            // Append initial case
            outputCode.Append("");

            // Boolean startCase. true = <? false \n%
            bool startCase = false;

            // Boolean which tells if the cursor is in the preparse or output part of the buffer (inside or outside an executable segment)
            bool codeMode = false;
            for(int i=0; i < input.Length ;i++)
            {
                 // Check if at an overgoing to an code block
                if(codeMode)
                {
                    if((startCase && input[i] == '?' && input[i+1] == '>') ||( input[i] == '\n' && !startCase))
                    {
                        codeMode=false;

                        // Jump forward two times if endcase is ?>
                        if(startCase)
                            i++;
                        
                        
                        // Append the code data to the string buffer
                        finalOutput.Append(" "+ executableSegment.ToString() + " ");
                        
                        // Clear outputcode buffer
                        executableSegment = new StringBuilder();

                        continue;
                    }
                    executableSegment.Append(input[i]);
                }
                else
                {
                    // If at end, summarize the string
                    if(i == input.Length - 1)
                    {
                        // Append the last string
                        outputCode.Append(input[i]);
                        // Format output code (replace " to ¤ and swap back on preprocessing)
                        String OutputCode = outputCode.ToString().Replace("\"", "¤").Replace("\n", "%BR%\");\n__printx(\"");
                        OutputCode = this.HandleToTokens(OutputCode.ToString(),'@');
                        finalOutput.Append("__printx(\"" + OutputCode + "\");");
                       
                    }
                    try
                    {
                        if (((input[i] == '\n' && input[i + 1] == '%')) || (input[i] == '<' && input[i + 1] == '?'))
                        {
                            startCase = (input[i] == '<' && input[i + 1] == '?');
                            codeMode = true;

                            // Convert tokens to interpretable handles
                            String OutputCode = outputCode.ToString().Replace("\"", "¤").Replace("\n", "%BR%\");\n__printx(\"");
                            OutputCode = this.HandleToTokens(OutputCode.ToString(), '@');
                            finalOutput.Append("__printx(\"" + OutputCode + "\");");

                            // Clear the output code buffer
                            outputCode = new StringBuilder();

                            // Skip two tokens forward to not include those tokens to the code buffer
                            i += 1;
                            continue;
                        }

                    }
                    catch
                    {
                        continue;
                    }
                    outputCode.Append(input[i]);
                    
                }
                

            }            
            // if exiting in text mode, append an end of the scalar string
            if (!parseMode)
            {
                CallStack += "\");";
            }
            // Run the code
            RuntimeMachine.SetFunction("__printx",new Func<String,object>( __printx));
            RuntimeMachine.SetFunction("synchronize_data", new Func<String, object>(synchronize_data));
            CallStack = finalOutput.ToString();
           
            CallStack = CallStack.Replace("\r", "");
            
            /***
             * Try run the page. If there was error return ERROR: <error> message so the
             * handler can choose to present it to the user
             * */
             try
             {
                 RuntimeMachine.Run(CallStack);
             }
            catch(Exception e)
             {
                return "ERROR: "+e.Message;
            }
            return this.Output;

        }
    }
}
