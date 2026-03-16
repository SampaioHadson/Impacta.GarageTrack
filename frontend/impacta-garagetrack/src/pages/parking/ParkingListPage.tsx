import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { LoadingMessage, ErrorMessage } from '../../components/common/Feedback'
import { ParkingCard } from '../../components/parking/ParkingCard'
import { parkingService } from '../../services/parkingService'
import type { ParkingRecord } from '../../types/parking'

export function ParkingListPage() {
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [records, setRecords] = useState<ParkingRecord[]>([])

  async function loadRecords() {
    setLoading(true)
    setError(null)

    try {
      const result = await parkingService.list()
      setRecords(result)
    } catch (loadError) {
      if (loadError instanceof Error) {
        setError(loadError.message)
      } else {
        setError('Nao foi possivel carregar os estacionamentos.')
      }
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    void loadRecords()
  }, [])

  return (
    <section className="section">
      <header className="section-header">
        <div>
          <h2>Estacionamentos</h2>
          <p>Lista de veiculos com entrada registrada.</p>
        </div>
        <div className="header-actions">
          <Link className="btn btn-secondary" to="/parking/new">
            Nova entrada
          </Link>
          <button className="btn btn-primary" onClick={() => void loadRecords()} type="button">
            Atualizar
          </button>
        </div>
      </header>

      {loading ? <LoadingMessage message="Carregando estacionamentos..." /> : null}
      {error ? <ErrorMessage message={error} /> : null}

      {!loading && !error && records.length === 0 ? (
        <p className="empty-state">Nenhum veiculo estacionado no momento.</p>
      ) : null}

      {!loading && !error && records.length > 0 ? (
        <div className="card-grid">
          {records.map((parking) => (
            <ParkingCard key={parking.id} parking={parking} />
          ))}
        </div>
      ) : null}
    </section>
  )
}
