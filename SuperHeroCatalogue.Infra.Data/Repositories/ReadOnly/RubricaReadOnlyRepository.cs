using SuperHeroCatalogue.Domain.Entities;
using SuperHeroCatalogue.Domain.Interfaces.Repositories.ReadOnly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace SuperHeroCatalogue.Infra.Data.Repositories.ReadOnly
{
    public class RubricaReadOnlyRepository : BaseReadOnlyRepository, IRubricaReadOnlyRepository
    {
        public Rubrica GetById(Guid id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT * FROM Rubrica
                            WHERE RubricaId = @sid and Ativo = 1";

                var rubrica = conn.Query<Rubrica>(sql, new { sid = id }).First();

                return rubrica;
            }
        }
        public IEnumerable<Rubrica> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT * FROM Rubrica WHERE ATIVO = 1
                            ORDER BY Codigo ASC";

                var rubricas = conn.Query<Rubrica>(sql);

                return rubricas;
            }
        }
        public IEnumerable<Rubrica> GetByFilter(Rubrica rubrica, string PalavraChave)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var Infotipos = string.Empty;
                var Incidencia = string.Empty;
                var FormaLancamento = string.Empty;
                var Processamento = string.Empty;
                var Habilitada = string.Empty;
                var Contabilizacao = string.Empty;
                var palavraChave = string.Empty;

                var Info0014 = rubrica.Info0014 == true ? 1 : 0;
                var Info0015 = rubrica.Info0015 == true ? 1 : 0;
                var Info0267 = rubrica.Info0267 == true ? 1 : 0;
                var Info2010 = rubrica.Info2010 == true ? 1 : 0;
                var flagINSS = rubrica.flagINSS == true ? 1 : 0;
                var flagFGTS = rubrica.flagFGTS == true ? 1 : 0;
                var flagIRRF = rubrica.flagIRRF == true ? 1 : 0;

                if (rubrica.Info0014 == true)
                {
                    Infotipos = String.Format("AND (Info0014 = {0})", Info0014);
                }
                if (rubrica.Info0015 == true)
                {
                    Infotipos = String.Format("AND (Info0015 = {0})", Info0015);
                }
                if (rubrica.Info0267 == true)
                {
                    Infotipos = String.Format("AND (Info0267 = {0})", Info0267);
                }
                if (rubrica.Info2010 == true)
                {
                    Infotipos = String.Format("AND (Info2010 = {0})", Info2010);
                }

                if (rubrica.flagINSS == true)
                {
                    Incidencia = String.Format("AND (flagINSS = {0})", flagINSS);
                }
                if (rubrica.flagFGTS == true)
                {
                    Incidencia = String.Format("AND (flagFGTS = {0})", flagFGTS);
                }
                if (rubrica.flagIRRF == true)
                {
                    Incidencia = String.Format("AND (flagIRRF = {0})", flagIRRF);
                }

                if (!String.IsNullOrEmpty(rubrica.FormaLancamento))
                {
                    FormaLancamento = String.Format("AND (FormaLancamento = '{0}')", rubrica.FormaLancamento);
                }
                if (!String.IsNullOrEmpty(rubrica.ProcessaFolha))
                {
                    Processamento = String.Format("AND (ProcessaFolha = '{0}')", rubrica.ProcessaFolha);
                }
                if (rubrica.ContaCredito > 0)
                {
                    Contabilizacao = String.Format("AND (ContaCredito = {0})", rubrica.ContaCredito);
                }
                if (rubrica.ContaDebito > 0)
                {
                    Contabilizacao = String.Format("AND (ContaDebito = {0})", rubrica.ContaDebito);
                }
                if (rubrica.flagRecisaoComplementar == true)
                {
                    Habilitada = String.Format("AND (flagRecisaoComplementar = {0})", rubrica.flagRecisaoComplementar == true ? 1 : 0);
                }
                if (!String.IsNullOrWhiteSpace(PalavraChave))
                {
                    palavraChave = String.Format("AND (codigo like '%{0}%' OR descricao like '%{0}%' OR descricaodetalhada like '%{0}%')", PalavraChave);
                }
                var sql = @"SELECT * FROM Rubrica
                            WHERE (1 = 1 AND ATIVO = 1 " + Infotipos + Incidencia + FormaLancamento + Processamento + Habilitada + Contabilizacao + ") " + palavraChave +
                                           " ORDER BY Codigo ASC";

                try
                {
                    var rubricas = conn.Query<Rubrica>(sql.Replace("\r\n", string.Empty));

                    return rubricas;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public void Remove(Guid id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"DELETE Rubrica WHERE RubricaId = @sid";

                conn.Query<Rubrica>(sql, new { sid = id });

                //return rubrica;
            }
        }

        public void Add(Rubrica rubrica)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql =
                @"
                INSERT INTO [dbo].[Rubrica]
                    ([RubricaId],[Codigo]
                    ,[Descricao],[DescricaoDetalhada]
                    ,[flagINSS],[flagIRRF]
                    ,[flagFGTS],[ContaCredito]
                    ,[ContaDebito],[FormaLancamento]
                    ,[ProcessaFolha],[Info0014]
                    ,[Info0015],[Info0267]
                    ,[Info2010],[flagRecisaoComplementar]
                    ,[ProcedimentoRegistroTicket]
                    ,[ProcedimentoRegistroTicketLink]
                    ,[DataCadastro],[DataAlteracao]
                    ,[Ativo])
                VALUES
                    (@sRubricaId
                    ,@sCodigo
                    ,@sDescricao
                    ,@sDescricaoDetalhada
                    ,@sflagINSS
                    ,@sflagIRRF
                    ,@sflagFGTS
                    ,@sContaCredito
                    ,@sContaDebito
                    ,@sFormaLancamento
                    ,@sProcessaFolha
                    ,@sInfo0014
                    ,@sInfo0015
                    ,@sInfo0267
                    ,@sInfo2010
                    ,@sflagRecisaoComplementar
                    ,@sProcedimentoRegistroTicket
                    ,@sProcedimentoRegistroTicketLink
                    ,@sDataCadastro
                    ,@sDataAlteracao
                    ,@sAtivo)";

                conn.Query<Rubrica>(sql, new
                {
                    sRubricaId = rubrica.RubricaId,
                    sCodigo = rubrica.Codigo,
                    sDescricao = rubrica.Descricao,
                    sDescricaoDetalhada = rubrica.DescricaoDetalhada,
                    sflagINSS = rubrica.flagINSS,
                    sflagIRRF = rubrica.flagIRRF,
                    sflagFGTS = rubrica.flagFGTS,
                    sContaCredito = rubrica.ContaCredito,
                    sContaDebito = rubrica.ContaDebito,
                    sFormaLancamento = rubrica.FormaLancamento,
                    sProcessaFolha = rubrica.ProcessaFolha,
                    sInfo0014 = rubrica.Info0014,
                    sInfo0015 = rubrica.Info0015,
                    sInfo0267 = rubrica.Info0267,
                    sInfo2010 = rubrica.Info2010,
                    sflagRecisaoComplementar = rubrica.flagRecisaoComplementar,
                    sProcedimentoRegistroTicket = rubrica.ProcedimentoRegistroTicket,
                    sProcedimentoRegistroTicketLink = rubrica.ProcedimentoRegistroTicketLink,
                    sDataCadastro = DateTime.Now,
                    sDataAlteracao = rubrica.DataAlteracao,
                    sAtivo = rubrica.Ativo
                });
            }
        }

        public void Update(Rubrica rubrica)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql =
                @"
                UPDATE [dbo].[Rubrica]
                   SET 
                     [Codigo] = @sCodigo
                    ,[Descricao] = @sDescricao
                    ,[DescricaoDetalhada] = @sDescricaoDetalhada
                    ,[flagINSS] = @sflagINSS
                    ,[flagIRRF] = @sflagIRRF
                    ,[flagFGTS] = @sflagFGTS
                    ,[ContaCredito] = @sContaCredito
                    ,[ContaDebito] = @sContaDebito
                    ,[FormaLancamento] = @sFormaLancamento
                    ,[ProcessaFolha] = @sProcessaFolha
                    ,[Info0014] = @sInfo0014
                    ,[Info0015] = @sInfo0015
                    ,[Info0267] = @sInfo0267
                    ,[Info2010] = @sInfo2010
                    ,[flagRecisaoComplementar] = @sflagRecisaoComplementar
                    ,[ProcedimentoRegistroTicket] = @sProcedimentoRegistroTicket
                    ,[ProcedimentoRegistroTicketLink] = @sProcedimentoRegistroTicketLink
                    ,[DataAlteracao] = @sDataAlteracao
                    ,[Ativo] = @sAtivo
                WHERE [RubricaId] = @sRubricaId";

                conn.Query<Rubrica>(sql, new
                {
                    sRubricaId = rubrica.RubricaId,
                    sCodigo = rubrica.Codigo,
                    sDescricao = rubrica.Descricao,
                    sDescricaoDetalhada = rubrica.DescricaoDetalhada,
                    sflagINSS = rubrica.flagINSS,
                    sflagIRRF = rubrica.flagIRRF,
                    sflagFGTS = rubrica.flagFGTS,
                    sContaCredito = rubrica.ContaCredito,
                    sContaDebito = rubrica.ContaDebito,
                    sFormaLancamento = rubrica.FormaLancamento,
                    sProcessaFolha = rubrica.ProcessaFolha,
                    sInfo0014 = rubrica.Info0014,
                    sInfo0015 = rubrica.Info0015,
                    sInfo0267 = rubrica.Info0267,
                    sInfo2010 = rubrica.Info2010,
                    sflagRecisaoComplementar = rubrica.flagRecisaoComplementar,
                    sProcedimentoRegistroTicket = rubrica.ProcedimentoRegistroTicket,
                    sProcedimentoRegistroTicketLink = rubrica.ProcedimentoRegistroTicketLink,
                    sDataAlteracao = DateTime.Now,
                    sAtivo = rubrica.Ativo
                });
            }
        }
    }
}
