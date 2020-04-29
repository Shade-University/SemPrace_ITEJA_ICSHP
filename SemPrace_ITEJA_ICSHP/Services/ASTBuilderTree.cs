using LanguageLogic;
using LanguageLogic.AST;
using LanguageLogic.AST.Statements;
using LanguageLogic.AST.Statements.Functions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace GUI.Model
{
    class ASTBuilderTree : INodeVisitor
    {
        private Parser parser;
        public ASTBuilderTree(Parser parser)
        {
            this.parser = parser;
        }

        public void BuildGraph(TreeView view)
        {
            Block root = parser.Parse();
            TreeViewItem rootItem = (TreeViewItem)root.Visit(this);
            view.Items.Add(rootItem);
        }

        public object Visit_AngleStatement(AngleStatement angleStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_Assign(AssignStatement node)
        { 
            TreeViewItem item = new TreeViewItem();
            item.Header = "Assign";

            TreeViewItem varItem = new TreeViewItem();
            varItem.Header = node.Variable.Identifier;
            varItem.Items.Add(node.Expression.Visit(this));

            item.Items.Add(varItem);

            return item;
        }

        public object Visit_BackwardStatement(BackwardStatement backwardStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_BinOp(BinOp node)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = node.Operation.Value;
            item.Items.Add(node.Left.Visit(this));
            item.Items.Add(node.Right.Visit(this));

            return item;

        }

        public object Visit_Block(Block node)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = "Block";

            TreeViewItem declItem = new TreeViewItem();
            declItem.Header = "Declarations";
            foreach (var decl in node.Declarations)
            {
                declItem.Items.Add(decl.Visit(this));
            }
            item.Items.Add(declItem);

            TreeViewItem childItem = new TreeViewItem();
            childItem.Header = "Statements";
            foreach (var child in node.BodyStatements)
            {
                childItem.Items.Add(child.Visit(this));
            }
            item.Items.Add(childItem);

            return item;
        }

        public object Visit_Condition(Condition condition)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = condition.Token.Value;
            item.Items.Add(condition.Left.Visit(this));
            item.Items.Add(condition.Right.Visit(this));

            return item;
        }

        public object Visit_ForStatement(ForStatement node)
        {
            throw new NotImplementedException();
        }

        public object Visit_ForwardStatement(ForwardStatement forwardStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_FuncCallStatement(FuncCallStatement funcCallStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_IfStatement(IfStatement ifStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_Num(Num node)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = node.Value.ToString();
            return item;
        }

        public object Visit_PenStatement(PenStatement penStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_StringText(StringText stringText)
        {
            throw new NotImplementedException();
        }

        public object Visit_UnaryOp(UnaryOp node)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = "UnaryOp " + node.Token.Value;
            item.Items.Add(node.Expression.Visit(this));

            return item;
        }

        public object Visit_Var(Var node)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = "Var " + node.Identifier;

            return item;
        }

        public object Visit_VarDeclaration(VarDeclaration node)
        {
            return null;
        }

        public object Visit_WhileStatement(WhileStatement whileStatement)
        {
            throw new NotImplementedException();
        }

        public object Visit_WriteStatement(WriteStatement writeStatement)
        {
            throw new NotImplementedException();
        }
    }
}
