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
    class ASTBuilderTree : INodeVisitor //Visitor which builds TreView
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
            rootItem.IsExpanded = true;

            view.Items.Add(rootItem);
        }

        public object Visit_AngleStatement(AngleStatement angleStatement)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "Angle";
            item.Items.Add(angleStatement.Angle.Visit(this));

            return item;
        }

        public object Visit_Assign(AssignStatement node)
        { 
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "Assign (=)";
            item.Items.Add(node.Variable.Visit(this));
            item.Items.Add(node.Expression.Visit(this));

            return item;
        }

        public object Visit_BackwardStatement(BackwardStatement backwardStatement)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "Backward";
            item.Items.Add(backwardStatement.Expression.Visit(this));

            return item;
        }

        public object Visit_BinOp(BinOp node)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = node.Operation.Value;
            item.Items.Add(node.Left.Visit(this));
            item.Items.Add(node.Right.Visit(this));

            return item;

        }

        public object Visit_Block(Block node)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;
            item.Header = "Block";

            node.Declarations.ForEach(x => item.Items.Add(x.Visit(this)));
            node.BodyStatements.ForEach(x => item.Items.Add(x.Visit(this)));

            return item;
        }

        public object Visit_Condition(Condition condition)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "Condition: " + condition.Token.Value;
            item.Items.Add(condition.Left.Visit(this));
            item.Items.Add(condition.Right.Visit(this));

            return item;
        }

        public object Visit_ForStatement(ForStatement node)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "For";
            item.Items.Add(node.FromExpression.Visit(this));
            item.Items.Add(node.ToExpression.Visit(this));
            item.Items.Add(node.BodyBlock.Visit(this));

            return item;
        }

        public object Visit_ForwardStatement(ForwardStatement forwardStatement)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = "Forward";
            item.IsExpanded = true;

            item.Items.Add(forwardStatement.Expression.Visit(this));

            return item;
        }

        public object Visit_FuncCallStatement(FuncCallStatement funcCallStatement)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "Func call";
            item.Items.Add(funcCallStatement.Function.Visit(this));

            return item;
        }

        public object Visit_IfStatement(IfStatement ifStatement)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "If";
            item.Items.Add(ifStatement.Condition.Visit(this));
            item.Items.Add(ifStatement.BodyBlock.Visit(this));

            return item;
        }

        public object Visit_Num(Num node)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = node.Value.ToString();
            return item;
        }

        public object Visit_PenStatement(PenStatement penStatement)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "Pen";
            item.Items.Add(penStatement.PenStatus.ToString());

            return item;
        }

        public object Visit_StringText(StringText stringText)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "\"" + stringText.Text.ToString() + "\"";
            return item;
        }

        public object Visit_UnaryOp(UnaryOp node)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "UnaryOp " + node.Token.Value;
            item.Items.Add(node.Expression.Visit(this));

            return item;
        }

        public object Visit_Var(Var node)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "Var " + node.Identifier + " TYPE: " + node.Type.ToString();

            return item;
        }

        public object Visit_VarDeclaration(VarDeclaration node)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "Variable declaration";
            item.Items.Add(node.Variable.Visit(this));

            return item;
        }

        public object Visit_WhileStatement(WhileStatement whileStatement)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "While";
            item.Items.Add(whileStatement.Condition.Visit(this));
            item.Items.Add(whileStatement.BodyBlock.Visit(this));

            return item;
        }

        public object Visit_WriteStatement(WriteStatement writeStatement)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = true;

            item.Header = "Write";
            item.Items.Add(writeStatement.Expression.Visit(this));

            return item;
        }
    }
}
