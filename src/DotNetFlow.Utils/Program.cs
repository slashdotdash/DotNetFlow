using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Infrastructure.Commanding;
using DotNetFlow.Core.Infrastructure.Eventing;
using DotNetFlow.Core.Services;
using Newtonsoft.Json;
using StructureMap;

namespace DotNetFlow.Utils
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var generator = new GuidCombGenerator();

            //var list = new List<Guid>();
            //for (var i = 0; i < 100; i++)
            //    list.Add(generator.GenerateNewId());

            //var x = list.ToJson();
            
            //new SerializeCommands().Execute();

            new DateTime(2011, 12, 2, 8, 39, 00).ToJson();

            new ImportCommands().Execute();
        }
    }
}
