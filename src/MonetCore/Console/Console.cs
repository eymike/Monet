using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonetCore.Console
{
    public class ConsoleCommand
    {
        public string CmdString { get; private set; }
        public string Help { get; private set; }

        private Func<string[], bool> m_invoker;

        public ConsoleCommand(string cmdString, string help, Func<string[], bool> invoker)
        {
            CmdString = cmdString;
            Help = help;
            m_invoker = invoker;
        }

        public bool Invoke(string[] args)
        {
            return m_invoker(args);
        }
    }

    [ServiceAtribute(Name = "Console")]
    public class Console
    {
        private readonly Thread m_messageThread;

        private Dictionary<string, ConsoleCommand> m_commands = new Dictionary<string, ConsoleCommand>();
       
        public Console(MonetServiceProvider services)
        {
            AddCommand("Help", "Prints list of commands", (string[] args) =>
            {
                if(args.Length == 1)
                {
                    string result = "\n";
                    foreach(var kvp in m_commands)
                    {
                        result += kvp.Value.CmdString + "\n";
                        Monet.LogMsg(result);
                    }
                }
                else if(args.Length == 2)
                {
                    var item = args[1].ToLower();
                    if(m_commands.ContainsKey(item))
                    {
                        Monet.LogMsg(m_commands[item].Help);
                    }
                    else
                    {
                        Monet.LogMsg("Help: Query not recognized.");
                    }
                }
                else
                {
                    Monet.LogMsg("Unknown parameters.");
                }
                return true;
            });

            m_messageThread = new Thread(this.MessageLoop);
            m_messageThread.Start();
        }

        public void MessageLoop()
        {
            bool running = true;
            while (running)
            {
                System.Console.Write(">");
                var command = System.Console.ReadLine();
                running = ParseCommand(command);
            }
        }

        public bool ParseCommand(string command)
        {
            var cmdAndParams = command.ToLower().Split(' ');
            bool retval = true;
            if (cmdAndParams.Length > 0 && cmdAndParams[0] != "")
            {
                if (m_commands.ContainsKey(cmdAndParams[0]))
                {
                    retval = m_commands[cmdAndParams[0]].Invoke(cmdAndParams);
                }
                else
                {
                    Monet.LogMsg("Command Not Recognized!");
                }
            }
            return retval;
        }

        public void AddCommand(string cmd, string help, Func<string[], bool> invoker)
        {
            var item = new ConsoleCommand(cmd.ToLower(), help, invoker);
            m_commands.Add(item.CmdString, item);
        }
    }
}
