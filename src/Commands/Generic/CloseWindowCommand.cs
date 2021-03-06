﻿using System;
using KarveCar.ViewModels;
using KarveCommon.Command;

namespace KarveCar.Commands.Generic
{
    class CloseWindowCommand : AbstractCommand
    {
        private CloseWindowViewModel closewindowvm;

        public CloseWindowCommand() {}

        public CloseWindowCommand(CloseWindowViewModel vm)
        {
            this.closewindowvm = vm;
        }
        public override  void Execute(object parameter)
        {
            closewindowvm.CloseWindow(parameter);         
        }

        public override bool UnExecute()
        {
            throw new NotImplementedException();
        }
    }
}
