import { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router-dom'
import { Button } from '../../components/common/Button'
import { ConfirmModal } from '../../components/common/ConfirmModal'
import { ErrorMessage, LoadingMessage } from '../../components/common/Feedback'
import { parkingService } from '../../services/parkingService'
import type { CloseParkingResult, ParkingRecord } from '../../types/parking'
import { formatDateTime } from '../../utils/format'

function formatMinutes(total: number): string {
  const hours = Math.floor(total / 60)
  const minutes = total % 60
  if (hours === 0) return `${minutes} min`
  if (minutes === 0) return `${hours}h`
  return `${hours}h ${minutes}min`
}

function formatBRL(value: number): string {
  return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })
}

export function ParkingDetailPage() {
  const { id } = useParams()
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [record, setRecord] = useState<ParkingRecord | null>(null)
  const [closing, setClosing] = useState(false)
  const [showConfirm, setShowConfirm] = useState(false)
  const [closeResult, setCloseResult] = useState<CloseParkingResult | null>(null)
  const [closeError, setCloseError] = useState<string | null>(null)

  useEffect(() => {
    const numericId = Number(id)

    if (!id || Number.isNaN(numericId)) {
      setError('Identificador de estacionamento invalido.')
      setLoading(false)
      return
    }

    async function loadRecord() {
      try {
        const result = await parkingService.getById(numericId)
        setRecord(result)
      } catch (loadError) {
        if (loadError instanceof Error) {
          setError(loadError.message)
        } else {
          setError('Nao foi possivel carregar o detalhe.')
        }
      } finally {
        setLoading(false)
      }
    }

    void loadRecord()
  }, [id])

  async function handleClose() {
    if (!record) return
    setClosing(true)
    setShowConfirm(false)
    setCloseError(null)

    try {
      const result = await parkingService.close(record.id)
      setCloseResult(result)
      setRecord((prev) => prev ? { ...prev, exitTime: result.exitTime, totalValue: result.totalValue } : prev)
    } catch (err) {
      if (err instanceof Error) {
        setCloseError(err.message)
      } else {
        setCloseError('Não foi possível encerrar o estacionamento.')
      }
    } finally {
      setClosing(false)
    }
  }

  return (
    <section className="section">
      <header className="section-header">
        <div>
          <h2>Detalhe do estacionamento</h2>
          <p>Visualize os dados completos do registro.</p>
        </div>
        <Link className="btn btn-secondary" to="/parking">
          Voltar para lista
        </Link>
      </header>

      {loading ? <LoadingMessage message="Carregando detalhe..." /> : null}
      {error ? <ErrorMessage message={error} /> : null}

      {!loading && !error && record ? (
        <>
          <article className="detail-card">
            <h3>{record.plate}</h3>
            <dl>
              <div>
                <dt>Cor</dt>
                <dd>{record.color}</dd>
              </div>
              <div>
                <dt>Entrada</dt>
                <dd>{formatDateTime(record.entryTime)}</dd>
              </div>
              <div>
                <dt>Saida</dt>
                <dd>{record.exitTime ? formatDateTime(record.exitTime) : '-'}</dd>
              </div>
              <div>
                <dt>Valor total</dt>
                <dd>{record.totalValue != null ? formatBRL(record.totalValue) : '-'}</dd>
              </div>
              <div>
                <dt>Criado por</dt>
                <dd>{record.createdByUserName}</dd>
              </div>
              <div>
                <dt>Finalizado por</dt>
                <dd>{record.finishedByUserName ?? '-'}</dd>
              </div>
            </dl>

            {!record.exitTime && !closeResult ? (
              <div style={{ marginTop: '1rem' }}>
                {closeError ? <ErrorMessage message={closeError} /> : null}
                <Button type="button" onClick={() => setShowConfirm(true)} disabled={closing}>
                  {closing ? 'Encerrando...' : 'Encerrar estacionamento'}
                </Button>
              </div>
            ) : null}
          </article>

          {closeResult ? (
            <article className="detail-card" style={{ marginTop: '1.5rem' }}>
              <h3>Resumo do encerramento</h3>
              <dl>
                <div>
                  <dt>Entrada</dt>
                  <dd>{formatDateTime(closeResult.entryTime)}</dd>
                </div>
                <div>
                  <dt>Saída</dt>
                  <dd>{formatDateTime(closeResult.exitTime)}</dd>
                </div>
                <div>
                  <dt>Tempo total</dt>
                  <dd>{formatMinutes(closeResult.totalMinutes)}</dd>
                </div>
              </dl>

              {closeResult.appliedTaxes.length > 0 ? (
                <>
                  <h4 style={{ marginTop: '1rem' }}>Tarifas aplicadas</h4>
                  <table className="data-table">
                    <thead>
                      <tr>
                        <th>Descrição</th>
                        <th>Valor</th>
                      </tr>
                    </thead>
                    <tbody>
                      {closeResult.appliedTaxes.map((tax, i) => (
                        <tr key={i}>
                          <td>{tax.description}</td>
                          <td>{formatBRL(tax.value)}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </>
              ) : (
                <p style={{ marginTop: '0.5rem', color: 'var(--color-muted, #888)' }}>
                  Nenhuma tarifa configurada para esta empresa.
                </p>
              )}

              <dl style={{ marginTop: '1rem' }}>
                <div>
                  <dt><strong>Valor total</strong></dt>
                  <dd><strong>{formatBRL(closeResult.totalValue)}</strong></dd>
                </div>
              </dl>
            </article>
          ) : null}
        </>
      ) : null}
      {showConfirm && record ? (
        <ConfirmModal
          title="Encerrar estacionamento"
          message={`Deseja encerrar o estacionamento da placa ${record.plate}? Esta ação não pode ser desfeita.`}
          confirmLabel="Encerrar"
          onConfirm={() => void handleClose()}
          onCancel={() => setShowConfirm(false)}
        />
      ) : null}
    </section>
  )
}
