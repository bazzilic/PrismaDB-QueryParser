﻿using Antlr4.Runtime.Misc;
using PrismaDB.QueryAST.DCL;
using PrismaDB.QueryAST.DDL;
using PrismaDB.QueryAST.DML;
using PrismaDB.QueryParser.MSSQL.AntlrGrammer;
using System.Collections.Generic;

namespace PrismaDB.QueryParser.MSSQL
{
    public partial class MsSqlVisitor : MsSqlParserBaseVisitor<object>
    {
        public override object VisitExportKeysCommand([NotNull] MsSqlParser.ExportKeysCommandContext context)
        {
            return new ExportKeysCommand(((StringConstant)Visit(context.stringLiteral())).strvalue);
        }

        public override object VisitUpdateKeysCommand([NotNull] MsSqlParser.UpdateKeysCommandContext context)
        {
            var res = new UpdateKeysCommand();
            if (context.STATUS() != null)
                res.StatusCheck = true;
            return res;
        }

        public override object VisitEncryptCommand([NotNull] MsSqlParser.EncryptCommandContext context)
        {
            var res = new EncryptColumnCommand();
            res.Column = (ColumnRef)Visit(context.fullColumnName());
            res.EncryptionFlags = ColumnEncryptionFlags.Store;
            if (context.encryptionOptions() != null)
                res.EncryptionFlags = (ColumnEncryptionFlags)Visit(context.encryptionOptions());
            if (context.STATUS() != null)
                res.StatusCheck = true;
            return res;
        }

        public override object VisitDecryptCommand([NotNull] MsSqlParser.DecryptCommandContext context)
        {
            var res = new DecryptColumnCommand();
            res.Column = (ColumnRef)Visit(context.fullColumnName());
            if (context.STATUS() != null)
                res.StatusCheck = true;
            return res;
        }

        public override object VisitRebalanceOpetreeCommand([NotNull] MsSqlParser.RebalanceOpetreeCommandContext context)
        {
            var res = new RebalanceOpetreeCommand();
            if (context.constants() != null)
                res.WithValues = (List<Constant>)Visit(context.constants());
            if (context.STATUS() != null)
                res.StatusCheck = true;
            return res;
        }

        public override object VisitSaveOpetreeCommand([NotNull] MsSqlParser.SaveOpetreeCommandContext context)
        {
            return new SaveOpetreeCommand();
        }

        public override object VisitLoadOpetreeCommand([NotNull] MsSqlParser.LoadOpetreeCommandContext context)
        {
            return new LoadOpetreeCommand();
        }

        public override object VisitLoadSchemaCommand([NotNull] MsSqlParser.LoadSchemaCommandContext context)
        {
            return new LoadSchemaCommand();
        }

        public override object VisitSaveSettingsCommand([NotNull] MsSqlParser.SaveSettingsCommandContext context)
        {
            return new SaveSettingsCommand();
        }

        public override object VisitLoadSettingsCommand([NotNull] MsSqlParser.LoadSettingsCommandContext context)
        {
            return new LoadSettingsCommand();
        }
    }
}