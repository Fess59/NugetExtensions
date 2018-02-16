using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example._0_Base.Data.Contexts;
using Example.Tests.CoreExample.Components;
using FessooFramework.Core;
using FessooFramework.Objects;
using FessooFramework.Objects.SourceData;
using FessooFramework.Tools.DataContexts;

namespace Example
{
    public class Bootstrapper : FessooFramework.Core.Bootstrapper<Bootstrapper>
    {
        public override string ApplicationName => "Application Example";

        public override void SetComponents(ref List<SystemComponent> components)
        {
           components.Add(new SingltonApplicationHelper());
        }

        public override void SetConfiguration(ref SystemCoreConfiguration settings)
        {
        }

        public override void SetDbContext(ref DataContextStore _Store)
        {
            //Удалённые
            _Store.Add<DefaultDB>("DefaultDB", "192.168.26.116", @"ExtUser", "123QWEasd");
            _Store.Add<DefaultDB2>("DefaultDB2", "192.168.26.116", @"ExtUser", "123QWEasd");
            _Store.Add<DefaultDB3>("DefaultDB_3", "192.168.26.116", @"ExtUser", "123QWEasd");
            _Store.Add<MainDB>("MainDB", "192.168.26.116", @"ExtUser", "123QWEasd");

            //Локальные
            //_Store.Add<DefaultDB>("DefaultDB");
            //_Store.Add<DefaultDB2>("DefaultDB2");
            //_Store.Add<DefaultDB3>("DefaultDB_3");
        }
    }
}
