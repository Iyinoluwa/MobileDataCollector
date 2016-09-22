﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToAndroidXML
{
    public class FieldItem
    {
        //public int fieldId { get; set; }
        public string name { get; set; }
        public string dataType { get; set; }
        public string pageName { get; set; }
        public bool IsIndexed { get; set; }
        public bool IsRequired { get; set; }
        public string Label { get; set; }
        public string fieldType { get; set; }
        //public List<string> options { get; set; }
    }
}
