import { Link } from 'react-router-dom'
import type { ParkingRecord } from '../../types/parking'
import { formatDateTime } from '../../utils/format'

interface ParkingCardProps {
  parking: ParkingRecord
}

export function ParkingCard({ parking }: ParkingCardProps) {
  return (
    <article className="parking-card">
      <header>
        <h3>{parking.plate}</h3>
        <span className={parking.exitTime ? 'status status-finished' : 'status status-open'}>
          {parking.exitTime ? 'Finalizado' : 'Em aberto'}
        </span>
      </header>

      <dl>
        <div>
          <dt>Cor</dt>
          <dd>{parking.color}</dd>
        </div>
        <div>
          <dt>Entrada</dt>
          <dd>{formatDateTime(parking.entryTime)}</dd>
        </div>
        <div>
          <dt>Criado por</dt>
          <dd>{parking.createdByUserName}</dd>
        </div>
      </dl>

      <Link className="card-link" to={`/parking/${parking.id}`}>
        Ver detalhes
      </Link>
    </article>
  )
}
