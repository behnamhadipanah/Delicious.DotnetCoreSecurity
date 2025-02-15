using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApp
{


    //IKeyManager,IKey Implemented ISecret
    public class CustomSecret : ISecret
    {
        public int Length => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void WriteSecretIntoBuffer(ArraySegment<byte> buffer)
        {
            throw new NotImplementedException();
        }
    }
}
