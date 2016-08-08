// Fill out your copyright notice in the Description page of Project Settings.

using UnrealBuildTool;
using System.IO;
using System;

public class OpenCV : ModuleRules
{
    private string ModulePath
    {
        get { return ModuleDirectory; }
    }

    public void addPreproc(string ros_preproc)
    {
        foreach (string s in ros_preproc.Split(';'))
        {
            Console.WriteLine("DEFINE:" + s);
            Definitions.Add(s);

        }
    }

    private string ThirdPartyPath
    {
        get { return Path.GetFullPath(Path.Combine(ModulePath, "../../ThirdParty/")); }
    }

    public void includeAdd(string env)
    {
        var items = Environment.GetEnvironmentVariable(env);
        foreach (string s in items.Split(';'))
        {
            Console.WriteLine("INCLUDE:" + s);
            PublicIncludePaths.Add(s);

        }

        

    }

    public void includeLib(string env, string prefix = null)
    {
        var items = Environment.GetEnvironmentVariable(env);
        string slib;

        if (prefix != null)
        {//TODO:make for non windows case
            if (prefix.PadRight(1) != "\\")
                prefix += '\\';
        }

        foreach (string s in items.Split(';'))
        {


            slib = prefix + s;
            Console.WriteLine("LIB INCLUDE:" + slib);
            PublicAdditionalLibraries.Add(slib);

        }
    }
	
	public void linkWithDirectory(string dir)
	{
		
		string[] fileEntries =Directory.GetFiles(dir, "*.lib");
		
		foreach (string fileName in fileEntries)
		{
		
			Console.WriteLine("OpenCV Link:{0}", fileName);
			PublicAdditionalLibraries.Add(fileName);
		
		}
		
		
	}

//this is all incomplete, right now, just brings in headers.  Clearly needs to bring in more.
    public OpenCV(TargetInfo Target)
    {
      
		Type = ModuleType.External;
		includeAdd("OPENCV_INCLUDE");
       // includeLib("OPENCV_LIBRARIES", Environment.GetEnvironmentVariable("OPENCV_LIB_DIR"));
	   
	   if (Target.Platform == UnrealTargetPlatform.Win64)
		{
	   
			linkWithDirectory(ModuleDirectory + @"\Lib\x64\" );
			
		}
		
	}

     
}
