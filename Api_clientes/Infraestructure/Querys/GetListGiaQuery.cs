using Api_clientes.Domain;
using MediatR;

namespace Api_clientes.Infraestructure.Querys
{
    public class GetListGiaQuery
    {
        public record ByGia(string gia) : IRequest<Response>;
        public class Handler : IRequestHandler<ByGia, Response>
        {
            private readonly AplicationDbContext _conex;

            public Handler(AplicationDbContext conex)
            {
                _conex = conex;
            }

            public async Task<Response> Handle(ByGia request, CancellationToken cancellationToken)
            {
                var cliente = _conex.Clientes.Where(x => x.gia == request.gia).First();
                return new Response(cliente);
            }
        }
        public record Response(Cliente Cliente);
    }
}
