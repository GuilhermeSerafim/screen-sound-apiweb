using Microsoft.AspNetCore.Components.Forms;
using System.Drawing;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System;
using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Request;

//  Imutabilidade: Por padrão, os records são imutáveis, o que significa que seus valores não podem ser alterados após a criação.Isso ajuda a garantir a integridade dos dados e a evitar efeitos colaterais indesejados.
//  Sintaxe Concisa: A declaração de um record é mais concisa do que a de uma classe tradicional, especialmente quando se deseja implementar igualdade de valor e imutabilidade.
//  Igualdade de Valor: Records implementam automaticamente a igualdade de valor, o que significa que dois records com os mesmos valores de propriedade são considerados iguais.
//  Desconstrução: Records suportam desconstrução, permitindo que você divida um record em suas partes constituintes facilmente.
//  Clonagem com with: Você pode criar cópias de um record com alterações específicas usando a expressão with.
public record MusicaRequest([Required] string nome, [Required] int ArtistaId, int anoLancamento, ICollection<GeneroRequest>? Generos = null);
