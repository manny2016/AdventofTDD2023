using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace TDD.Day10
{
    public class VertexEqualityComparer : IEqualityComparer<Vertex>
    {
        public static readonly VertexEqualityComparer Instance = new VertexEqualityComparer();
        public bool Equals(Vertex? x, Vertex? y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] Vertex obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
