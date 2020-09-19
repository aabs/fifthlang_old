using System.Collections.Generic;

namespace fifth.Parser.AST.Builders
{
    public class ArgumentListBuilder : ListBuilder<Argument> { }

    public class ExpressionListBuilder : ListBuilder<Expr> { }

    public class ListBuilder<TItem>
    {
        public ListBuilder()
        {
            this.Items = new List<TItem>();
        }

        public List<TItem> Items { get; private set; }

        public static ListBuilder<TItem> Start()
        {
            return new ListBuilder<TItem>();
        }

        public List<TItem> Build()
        {
            return this.Items;
        }

        public ListBuilder<TItem> WithItem(TItem item)
        {
            this.Items.Add(item);
            return this;
        }
    }

    public class ParameterDeclarationListBuilder : ListBuilder<ParameterDeclaration> { }
}