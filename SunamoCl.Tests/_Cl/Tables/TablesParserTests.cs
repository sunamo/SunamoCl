namespace cmd.Tests.Tables

    class TablesParserTests
    {
        public void ToStringTable()
        {
            var els = "Extra really long string";

            var l1 = CAG.ToList<string>("A", els, "", "");
            var l2 = CAG.ToList<string>("B", "", els, "");

            //List<List<string>> ls = new List<List<string>>();
            //ls.Add(l1);
            //ls.Add(l2);

            var headers = TestData.listABCD;

            var list = new List<string>();
            list.AddRange(l1);
            list.AddRange(l2);

            var td = CA.OneDimensionArrayToTwoDirection(list.ToArray(), 4);

            //string[,] t = new string[]

            //cl.CmdTableTest();
            //CmdTableTests.CmdTable2Tests();

            var s = TableParser.ToStringTable(td);
            CL.WriteLine(s);
        }
    }
}
