using cali.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace cali_vm.Command.dmy.modules
{
    public class Modules
    { 
        public Modules(string name, string desc, string type, string typedesc, string typename, string pubon, string pubby)
        {
            this.Name = name.ToString();
            this.Description = desc.ToString();

            this.Type = type.ToString();
            this.TypeDescription = typedesc.ToString();
            this.TypeName = typename.ToString();

            this.PublishedOn = pubon.ToString();
            this.PublishedBy = pubby.ToString();
        }
        public string Name;
        public string Description;
        public string Type;

        public string TypeDescription;
        public string TypeName;
        public string PublishedOn;
        public string PublishedBy;

        public string Get(string item)
        {
            string res = "";

            if (item != null)
            {
                if (item.ToLower() == "name")
                {
                    res = this.Name;
                }
                else if (item.ToLower() == "desc")
                {
                    res = this.Description;
                }
                else if (item.ToLower() == "type")
                {
                    res = this.Type;
                }
                else if (item.ToLower() == "typedesc")
                {
                    res = this.TypeDescription;
                }
                else if (item.ToLower() == "typename")
                {
                    res = this.TypeName;
                }
                else if (item.ToLower() == "pubby")
                {
                    res = this.PublishedBy;
                }
                else if (item.ToLower() == "pubon")
                {
                    res = this.PublishedOn;
                }
                else 
                {
                    res = this.Name;
                }
            }

            return res;
        }
    }

    public class DmyInit
    {
        public static List<Modules> Init()
        {
            List<Modules> Mods = new List<Modules>();

            string MyName = "hmza";
            
            { 
                Modules thix = new Modules("torpedo","A module to scrape .onion network.","DARK","used to scrape ad find vlnarablities in a .onion site.","DARK-WEB","28-SEP-2024",MyName);
                Mods.Add(thix);
            }

            return Mods;
        }
    }


    public class usmansploit
    {
        public static List<cali_vm.Command.dmy.modules.Modules> Mods = new List<cali_vm.Command.dmy.modules.Modules>();
        public static void ProcessDmyCommand(string[] parts)
        {
            if (parts[0] == "@dmy")
            {
                if (parts.Length <= 0) return;

                if (parts[1].StartsWith("mod@"))
                {
                    if (parts[1].StartsWith("mod@latest@"))
                    {
                        if (parts[1].Equals("mod@latest@list"))
                        {
                            cali.Utils.caliOutput.ModOut.ListModules(Mods);
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (parts[1].StartsWith("mod@start@"))
                    {
                        if (parts[1].Equals("mod@latest@torpedo"))
                        {
                            cali.Utils.caliOutput.ModOut.ListModules(Mods);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        errs.CacheClean();
                        errs.New($"`{parts[1]}` is not a real comand associated with `@dmy`!");
                        errs.ListThem();
                    }
                }
                else
                {
                    errs.CacheClean();
                    errs.New($"`{parts[1]}` is not a real comand associated with `@dmy`!");
                    errs.ListThem();
                }
            }
            else
            {
                errs.CacheClean();
                errs.New($"`{parts[0]}` is not a real command, related to `@dmy` systems");
                errs.ListThemAll();
            }
        }

    }
}
