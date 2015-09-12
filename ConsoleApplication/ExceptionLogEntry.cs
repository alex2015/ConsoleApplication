using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication.Visitor;

namespace ConsoleApplication
{
    public class ExceptionLogEntry : LogEntryBase
    {
        public override void Accept(ILogEntryVisitor logEntryVisitor)
        {
            // Благодаря перегрузке методов выбирается метод
            // Visit(ExceptionLogEntry)
            logEntryVisitor.Visit(this);
        }
    }
}
