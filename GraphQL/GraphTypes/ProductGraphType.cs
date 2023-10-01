﻿using GraphQL.Types;
using Zerno.Models;

namespace Zerno.GraphQL.GraphTypes
{
    public class ProductGraphType : ObjectGraphType<Product>
    {
        public ProductGraphType() 
        { 
            Name = "product";
            Field(p => p.Price).Description("Цена товара");
            Field(p => p.FullAmmount).Description("Количество товара к началу продажи");
            Field(p => p.Ammount).Description("Оставшееся количество продукции");
            Field(p => p.Type).Description("Тип товара");
            Field(p => p.StartDate).Description("Даата начала продаж");
            Field(p => p.EndDate).Description("Дата окончания продаж");
        }
    }
}
