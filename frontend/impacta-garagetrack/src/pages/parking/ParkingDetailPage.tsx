import { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router-dom'
import { ErrorMessage, LoadingMessage } from '../../components/common/Feedback'
import { parkingService } from '../../services/parkingService'
import type { ParkingRecord } from '../../types/parking'
import { formatDateTime } from '../../utils/format'

export function ParkingDetailPage() {
  const { id } = useParams()
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [record, setRecord] = useState<ParkingRecord | null>(null)

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
              <dd>{formatDateTime(record.exitTime)}</dd>
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
        </article>
      ) : null}
    </section>
  )
}
