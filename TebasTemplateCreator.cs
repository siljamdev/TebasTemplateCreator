using System;
using AshLib;

class TebasTemplateCreator{
	
	static AshFile t;
	
	static void Main(string[] args){
		Console.WriteLine("Welcome!");
		Console.WriteLine();
		
		t = new AshFile();
		string name = ask("Name of the template:");
		t.SetCamp("name", name);
		
		t.SetCamp("git.defaultUse", askTF("Uses git?:"));
		
		loadFile("default.gitignore", "git.gitignore");
		
		if(File.Exists("readme.md")){
			t.SetCamp("addReadme", true);
			loadFile("readme.md", "readme");
		}else{
			t.SetCamp("addReadme", askTF("Add readme file?"));
		}
		
		
		loadFile("extensions.txt", "codeExtensions");
		
		if(Directory.Exists("scripts")){
			string[] scripts = Directory.GetFiles("scripts", "*.tbscr", SearchOption.TopDirectoryOnly);
			
			foreach(string scr in scripts){
				t.SetCamp("script." + Path.GetFileNameWithoutExtension(scr), File.ReadAllText(scr));
				Console.WriteLine("Loaded script " + Path.GetFileNameWithoutExtension(scr));
			}
		}else{
			Console.WriteLine("We could not find the scripts folder");
		}
		
		if(Directory.Exists("resources")){
			string[] resources = Directory.GetFiles("resources", "*.*", SearchOption.TopDirectoryOnly);
			
			foreach(string res in resources){
				t.SetCamp("resources." + Path.GetFileNameWithoutExtension(res), File.ReadAllText(res));
				Console.WriteLine("Loaded resource " + res);
			}
		}else{
			Console.WriteLine("We could not find the resources folder");
		}
		
		t.Save(name + ".tbtem");
		
		Console.WriteLine("Bye!");
	}
	
	static string ask(string q){
		Console.Write(q + " ");
		return Console.ReadLine();
	}
	
	static bool askTF(string q){
		string s;
		do{
			s = ask(q).ToLower();
		}while(s != "true" && s != "false");
		
		return (s == "true" ? true : false);
	}
	
	static void loadFile(string p, string o){
		if(File.Exists(p)){
			t.SetCamp(o, File.ReadAllText(p));
			Console.WriteLine("Read file " + p);
		}
	}
}