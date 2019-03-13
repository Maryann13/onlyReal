﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
	class ChangeVisitor : AutoVisitor
	{
		public void ReplaceExpr(ExprNode from, ExprNode to)
		{
			var p = from.Parent;
			if (to != null)
				to.Parent = p;
			if (p is AssignNode assn)
			{
				assn.Expr = to;
			}
			else if (p is BinOpNode binopn)
			{
				if (binopn.Left == from) // Поиск подузла в Parent
					binopn.Left = to;
				else if (binopn.Right == from)
					binopn.Right = to;
			}
			else if (p is ForNode forn)
			{
				if (forn.Start == from) // Поиск подузла в Parent
					forn.Start = to;
				else if (forn.End == from)
					forn.End = to;
			}
			else if (p is PrintlnNode pn)
			{
				if (pn.Expr == from) // Поиск подузла в Parent
					pn.Expr = to;
			}
			else if (p is BlockNode)
			{
				throw new Exception("Родительский узел не содержит выражений");
			}
		}
		public void ReplaceStat(StatementNode from, StatementNode to)
		{
			var p = from.Parent;
			if (p is AssignNode || p is ExprNode)
			{
				throw new Exception("Родительский узел не содержит операторов");
			}
			if (to != null)
				to.Parent = p;
			if (p is BlockNode bln) // Можно переложить этот код на узлы!
			{
				for (var i = 0; i < bln.StList.Count - 1; i++)
					if (bln.StList[i] == from)
						bln.StList[i] = to;
			}
			else if (p is IfNode ifn)
			{
				if (ifn.If == from) // Поиск подузла в Parent
					ifn.If = to;
				else if (ifn.Else == from)
					ifn.Else = to;
			}
			/*else if (p is ForNode forn)
			{
				if (forn.Start == from) // Поиск подузла в Parent
					forn.If = to;
				else if (forn.Else == from)
					forn.Else = to;
			}*/
		}
	}
}