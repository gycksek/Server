﻿
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace PacketGenerator
{
	class Program
	{
		static string clientRegister;
		static string serverRegister;

		static void Main(string[] args)
		{
			string file = "../../../../../../Common/protoc-3.19.4-win64/bin/Protocol.proto";
			if (args.Length >= 1)
				file = args[0];

			bool startParsing = false;
			foreach (string line in File.ReadAllLines(file))
			{
				if (!startParsing && line.Contains("enum MsgId"))
				{
					startParsing = true;
					continue;
				}

				if (!startParsing)
					continue;

				if (line.Contains("}"))
					break;

				string[] names = line.Trim().Split(" =");
				if (names.Length == 0)
					continue;

				string name = names[0];
				if (name.StartsWith("S2C_"))
				{
					string[] words = name.Split("_");

					string msgName = "";
					string packetName = "";
					foreach (string word in words)
					{
						//msgName += FirstCharToUpper(word);
						string temptPacketName = FirstCharToUpper(word);
						msgName += temptPacketName;
						packetName += temptPacketName + "_";
						temptPacketName = "";
					}
					int packetNameLen = packetName.Length;
					//string packetName = $"S2C_{msgName.Substring(3)}";
					clientRegister += string.Format(PacketFormat.managerRegisterFormat, msgName, packetName.Substring(0, packetNameLen - 1));
				}
				else if (name.StartsWith("C2S_"))
				{
					string[] words = name.Split("_");

					string msgName = "";
					string packetName = "";
					foreach (string word in words)
					{
						//msgName += FirstCharToUpper(word);
						string temptPacketName = FirstCharToUpper(word);
						msgName += temptPacketName;
						packetName += temptPacketName + "_";
						temptPacketName = "";
					}
					int packetNameLen = packetName.Length;
					
					//string packetName = $"C2S_{msgName.Substring(3)}";
					serverRegister += string.Format(PacketFormat.managerRegisterFormat, msgName, packetName.Substring(0,packetNameLen-1));
				}
			}

			string clientManagerText = string.Format(PacketFormat.managerFormat, clientRegister);
			File.WriteAllText("ClientPacketManager2.cs", clientManagerText);
			string serverManagerText = string.Format(PacketFormat.managerFormat, serverRegister);
			File.WriteAllText("ServerPacketManager2.cs", serverManagerText);
		}

		public static string FirstCharToUpper(string input)
		{
			if (string.IsNullOrEmpty(input))
				return "";

			if (input.StartsWith("C2S"))
            {
				return input;
			}
			else if(input.StartsWith("S2C"))
            {
				return input;
			}
			else
			 return input[0].ToString().ToUpper() + input.Substring(1).ToLower();
		}
	}
}
