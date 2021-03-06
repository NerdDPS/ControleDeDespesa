﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Modelos
{
    public class TiposDeDespesas
    {
        public virtual int Id { get; set; }

        [Required]
        public virtual string Descricao { get; set; }
        public virtual double ValorFixo { get; set; }
    }
}