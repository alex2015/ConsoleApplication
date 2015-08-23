using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace ConsoleApplication
{
    [ContractClass(typeof(LogImporterContract))]
    public abstract class LogImporter
    {
        protected abstract IEnumerable<string> ReadEntries(ref int position);
        protected abstract LogEntry ParseLogEntry(string stringEntry);
    }

    [ExcludeFromCodeCoverage,ContractClassFor(typeof(LogImporter))]
    public abstract class LogImporterContract : LogImporter
    {
        protected override IEnumerable<string> ReadEntries(ref int position)
        {
            Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
            Contract.Ensures(Contract.ValueAtReturn(out position) >= Contract.OldValue(position));

            throw new NotImplementedException();
        }

        protected override LogEntry ParseLogEntry(string stringEntry)
        {
            Contract.Requires(stringEntry != null);
            Contract.Ensures(Contract.Result<LogEntry>() != null);

            throw new NotImplementedException();
        }
    }
}
