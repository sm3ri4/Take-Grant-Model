﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeGrantModel.Commands{
    public interface ICommand{
        public void Execute(string[] args);
    }
}
