using System;
using System.Collections.Generic;

namespace TiendaVirtual.Entidades
{
    public class Usuario : IUsuario, IEquatable<Usuario>
    {
        public int Id { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }

        public Usuario() { }

        public Usuario(int id, string nick, string password)
        {
            Id = id;
            Nick = nick ?? throw new ArgumentNullException(nameof(nick));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", Id, Nick, Password);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Usuario);
        }

        public bool Equals(Usuario other)
        {
            return other != null &&
                   Id == other.Id &&
                   Nick == other.Nick &&
                   Password == other.Password;
        }

        public override int GetHashCode()
        {
            var hashCode = -1169676298;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nick);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            return hashCode;
        }

        public static bool operator ==(Usuario usuario1, Usuario usuario2)
        {
            return EqualityComparer<Usuario>.Default.Equals(usuario1, usuario2);
        }

        public static bool operator !=(Usuario usuario1, Usuario usuario2)
        {
            return !(usuario1 == usuario2);
        }
    }
}
