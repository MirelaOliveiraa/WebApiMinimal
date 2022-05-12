namespace WebApiMinimal.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Comentario { get; set; }

        public Carro(string Nome, string Comentario)
        {
            this.Nome = Nome;

            this.Comentario = Comentario;
        }
    }
}
