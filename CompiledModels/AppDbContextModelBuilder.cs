﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

#pragma warning disable 219, 612, 618
#nullable disable

namespace MegaSync.CompiledModels
{
    public partial class AppDbContextModel
    {
        partial void Initialize()
        {
            var logMessage = LogMessageEntityType.Create(this);
            var megaLink = MegaLinkEntityType.Create(this);

            LogMessageEntityType.CreateAnnotations(logMessage);
            MegaLinkEntityType.CreateAnnotations(megaLink);

            AddAnnotation("ProductVersion", "8.0.3");
            AddRuntimeAnnotation("Relational:RelationalModel", CreateRelationalModel());
        }

        private IRelationalModel CreateRelationalModel()
        {
            var relationalModel = new RelationalModel(this);

            var logMessage = FindEntityType("MegaSync.Model.LogMessage")!;

            var defaultTableMappings = new List<TableMappingBase<ColumnMappingBase>>();
            logMessage.SetRuntimeAnnotation("Relational:DefaultMappings", defaultTableMappings);
            var megaSyncModelLogMessageTableBase = new TableBase("MegaSync.Model.LogMessage", null, relationalModel);
            var idColumnBase = new ColumnBase<ColumnMappingBase>("Id", "INTEGER", megaSyncModelLogMessageTableBase);
            megaSyncModelLogMessageTableBase.Columns.Add("Id", idColumnBase);
            var messageColumnBase = new ColumnBase<ColumnMappingBase>("Message", "TEXT", megaSyncModelLogMessageTableBase);
            megaSyncModelLogMessageTableBase.Columns.Add("Message", messageColumnBase);
            var timestampColumnBase = new ColumnBase<ColumnMappingBase>("Timestamp", "TEXT", megaSyncModelLogMessageTableBase);
            megaSyncModelLogMessageTableBase.Columns.Add("Timestamp", timestampColumnBase);
            relationalModel.DefaultTables.Add("MegaSync.Model.LogMessage", megaSyncModelLogMessageTableBase);
            var megaSyncModelLogMessageMappingBase = new TableMappingBase<ColumnMappingBase>(logMessage, megaSyncModelLogMessageTableBase, true);
            megaSyncModelLogMessageTableBase.AddTypeMapping(megaSyncModelLogMessageMappingBase, false);
            defaultTableMappings.Add(megaSyncModelLogMessageMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)idColumnBase, logMessage.FindProperty("Id")!, megaSyncModelLogMessageMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)messageColumnBase, logMessage.FindProperty("Message")!, megaSyncModelLogMessageMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)timestampColumnBase, logMessage.FindProperty("Timestamp")!, megaSyncModelLogMessageMappingBase);

            var tableMappings = new List<TableMapping>();
            logMessage.SetRuntimeAnnotation("Relational:TableMappings", tableMappings);
            var logMessagesTable = new Table("LogMessages", null, relationalModel);
            var idColumn = new Column("Id", "INTEGER", logMessagesTable);
            logMessagesTable.Columns.Add("Id", idColumn);
            var messageColumn = new Column("Message", "TEXT", logMessagesTable);
            logMessagesTable.Columns.Add("Message", messageColumn);
            var timestampColumn = new Column("Timestamp", "TEXT", logMessagesTable);
            logMessagesTable.Columns.Add("Timestamp", timestampColumn);
            var pK_LogMessages = new UniqueConstraint("PK_LogMessages", logMessagesTable, new[] { idColumn });
            logMessagesTable.PrimaryKey = pK_LogMessages;
            var pK_LogMessagesUc = RelationalModel.GetKey(this,
                "MegaSync.Model.LogMessage",
                new[] { "Id" });
            pK_LogMessages.MappedKeys.Add(pK_LogMessagesUc);
            RelationalModel.GetOrCreateUniqueConstraints(pK_LogMessagesUc).Add(pK_LogMessages);
            logMessagesTable.UniqueConstraints.Add("PK_LogMessages", pK_LogMessages);
            var iX_LogMessages_Timestamp = new TableIndex(
            "IX_LogMessages_Timestamp", logMessagesTable, new[] { timestampColumn }, false);
            var iX_LogMessages_TimestampIx = RelationalModel.GetIndex(this,
                "MegaSync.Model.LogMessage",
                new[] { "Timestamp" });
            iX_LogMessages_Timestamp.MappedIndexes.Add(iX_LogMessages_TimestampIx);
            RelationalModel.GetOrCreateTableIndexes(iX_LogMessages_TimestampIx).Add(iX_LogMessages_Timestamp);
            logMessagesTable.Indexes.Add("IX_LogMessages_Timestamp", iX_LogMessages_Timestamp);
            relationalModel.Tables.Add(("LogMessages", null), logMessagesTable);
            var logMessagesTableMapping = new TableMapping(logMessage, logMessagesTable, true);
            logMessagesTable.AddTypeMapping(logMessagesTableMapping, false);
            tableMappings.Add(logMessagesTableMapping);
            RelationalModel.CreateColumnMapping(idColumn, logMessage.FindProperty("Id")!, logMessagesTableMapping);
            RelationalModel.CreateColumnMapping(messageColumn, logMessage.FindProperty("Message")!, logMessagesTableMapping);
            RelationalModel.CreateColumnMapping(timestampColumn, logMessage.FindProperty("Timestamp")!, logMessagesTableMapping);

            var megaLink = FindEntityType("MegaSync.Model.MegaLink")!;

            var defaultTableMappings0 = new List<TableMappingBase<ColumnMappingBase>>();
            megaLink.SetRuntimeAnnotation("Relational:DefaultMappings", defaultTableMappings0);
            var megaSyncModelMegaLinkTableBase = new TableBase("MegaSync.Model.MegaLink", null, relationalModel);
            var lastUpdatedColumnBase = new ColumnBase<ColumnMappingBase>("LastUpdated", "TEXT", megaSyncModelMegaLinkTableBase);
            megaSyncModelMegaLinkTableBase.Columns.Add("LastUpdated", lastUpdatedColumnBase);
            var pathColumnBase = new ColumnBase<ColumnMappingBase>("Path", "TEXT", megaSyncModelMegaLinkTableBase);
            megaSyncModelMegaLinkTableBase.Columns.Add("Path", pathColumnBase);
            var urlColumnBase = new ColumnBase<ColumnMappingBase>("Url", "TEXT", megaSyncModelMegaLinkTableBase);
            megaSyncModelMegaLinkTableBase.Columns.Add("Url", urlColumnBase);
            relationalModel.DefaultTables.Add("MegaSync.Model.MegaLink", megaSyncModelMegaLinkTableBase);
            var megaSyncModelMegaLinkMappingBase = new TableMappingBase<ColumnMappingBase>(megaLink, megaSyncModelMegaLinkTableBase, true);
            megaSyncModelMegaLinkTableBase.AddTypeMapping(megaSyncModelMegaLinkMappingBase, false);
            defaultTableMappings0.Add(megaSyncModelMegaLinkMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)urlColumnBase, megaLink.FindProperty("Url")!, megaSyncModelMegaLinkMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)lastUpdatedColumnBase, megaLink.FindProperty("LastUpdated")!, megaSyncModelMegaLinkMappingBase);
            RelationalModel.CreateColumnMapping((ColumnBase<ColumnMappingBase>)pathColumnBase, megaLink.FindProperty("Path")!, megaSyncModelMegaLinkMappingBase);

            var tableMappings0 = new List<TableMapping>();
            megaLink.SetRuntimeAnnotation("Relational:TableMappings", tableMappings0);
            var megaLinksTable = new Table("MegaLinks", null, relationalModel);
            var urlColumn = new Column("Url", "TEXT", megaLinksTable);
            megaLinksTable.Columns.Add("Url", urlColumn);
            var lastUpdatedColumn = new Column("LastUpdated", "TEXT", megaLinksTable);
            megaLinksTable.Columns.Add("LastUpdated", lastUpdatedColumn);
            var pathColumn = new Column("Path", "TEXT", megaLinksTable);
            megaLinksTable.Columns.Add("Path", pathColumn);
            var pK_MegaLinks = new UniqueConstraint("PK_MegaLinks", megaLinksTable, new[] { urlColumn });
            megaLinksTable.PrimaryKey = pK_MegaLinks;
            var pK_MegaLinksUc = RelationalModel.GetKey(this,
                "MegaSync.Model.MegaLink",
                new[] { "Url" });
            pK_MegaLinks.MappedKeys.Add(pK_MegaLinksUc);
            RelationalModel.GetOrCreateUniqueConstraints(pK_MegaLinksUc).Add(pK_MegaLinks);
            megaLinksTable.UniqueConstraints.Add("PK_MegaLinks", pK_MegaLinks);
            var iX_MegaLinks_Path = new TableIndex(
            "IX_MegaLinks_Path", megaLinksTable, new[] { pathColumn }, true);
            var iX_MegaLinks_PathIx = RelationalModel.GetIndex(this,
                "MegaSync.Model.MegaLink",
                new[] { "Path" });
            iX_MegaLinks_Path.MappedIndexes.Add(iX_MegaLinks_PathIx);
            RelationalModel.GetOrCreateTableIndexes(iX_MegaLinks_PathIx).Add(iX_MegaLinks_Path);
            megaLinksTable.Indexes.Add("IX_MegaLinks_Path", iX_MegaLinks_Path);
            relationalModel.Tables.Add(("MegaLinks", null), megaLinksTable);
            var megaLinksTableMapping = new TableMapping(megaLink, megaLinksTable, true);
            megaLinksTable.AddTypeMapping(megaLinksTableMapping, false);
            tableMappings0.Add(megaLinksTableMapping);
            RelationalModel.CreateColumnMapping(urlColumn, megaLink.FindProperty("Url")!, megaLinksTableMapping);
            RelationalModel.CreateColumnMapping(lastUpdatedColumn, megaLink.FindProperty("LastUpdated")!, megaLinksTableMapping);
            RelationalModel.CreateColumnMapping(pathColumn, megaLink.FindProperty("Path")!, megaLinksTableMapping);
            return relationalModel.MakeReadOnly();
        }
    }
}
