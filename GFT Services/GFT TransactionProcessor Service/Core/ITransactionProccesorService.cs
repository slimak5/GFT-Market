using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Messaging;

namespace GFT.Services.TransactionProcessor
{
    [ServiceContract]
    public interface ITransactionProcessor
    {
        [OperationContract]
        void StartMainLoop();

        [OperationContract]
        void StopMainLoop();

        [OperationContract]
        System.Threading.ThreadState GetWorkerThreadState();

    }

}

