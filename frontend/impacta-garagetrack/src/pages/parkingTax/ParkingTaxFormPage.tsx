import { useEffect, useState } from 'react'
import { Link, useLocation, useNavigate, useParams } from 'react-router-dom'
import { Button } from '../../components/common/Button'
import { ErrorMessage, LoadingMessage } from '../../components/common/Feedback'
import { Input } from '../../components/common/Input'
import { parkingTaxService } from '../../services/parkingTaxService'
import type { ParkingTax, ParkingTaxType } from '../../types/parkingTax'
import { PARKING_TAX_TYPE_LABELS } from '../../types/parkingTax'

const TAX_TYPES: ParkingTaxType[] = ['Hour', 'AfterHour', 'Daily']

interface FormState {
  type: ParkingTaxType
  minutes: string
  value: string
}

const EMPTY_FORM: FormState = { type: 'Hour', minutes: '', value: '' }

function taxToForm(tax: ParkingTax): FormState {
  return {
    type: tax.type,
    minutes: tax.minutes !== null ? String(tax.minutes) : '',
    value: String(tax.value),
  }
}

export function ParkingTaxFormPage() {
  const { id } = useParams()
  const navigate = useNavigate()
  const location = useLocation()

  const isEditing = Boolean(id)
  const stateItem = (location.state as { tax?: ParkingTax } | null)?.tax

  const [loadingRecord, setLoadingRecord] = useState(isEditing && !stateItem)
  const [form, setForm] = useState<FormState>(stateItem ? taxToForm(stateItem) : EMPTY_FORM)
  const [submitting, setSubmitting] = useState(false)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    if (!isEditing || stateItem) return

    async function loadForEdit() {
      try {
        const taxes = await parkingTaxService.list()
        const found = taxes.find((t) => t.id === Number(id))

        if (!found) {
          setError('Tarifa não encontrada.')
        } else {
          setForm(taxToForm(found))
        }
      } catch (loadError) {
        if (loadError instanceof Error) {
          setError(loadError.message)
        } else {
          setError('Não foi possível carregar a tarifa.')
        }
      } finally {
        setLoadingRecord(false)
      }
    }

    void loadForEdit()
  }, [id, isEditing, stateItem])

  function handleTypeChange(type: ParkingTaxType) {
    setForm((prev) => ({
      ...prev,
      type,
      minutes: type === 'Hour' ? prev.minutes : '',
    }))
  }

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault()
    setError(null)

    const parsedValue = parseFloat(form.value.replace(',', '.'))
    if (Number.isNaN(parsedValue) || parsedValue <= 0) {
      setError('Informe um valor válido maior que zero.')
      return
    }

    const parsedMinutes =
      form.type === 'Hour' ? parseInt(form.minutes, 10) : null

    if (form.type === 'Hour' && (Number.isNaN(parsedMinutes as number) || (parsedMinutes as number) <= 0)) {
      setError('Informe o tempo (minutos) para o tipo Hora.')
      return
    }

    setSubmitting(true)

    try {
      if (isEditing) {
        await parkingTaxService.update(Number(id), {
          type: form.type,
          minutes: parsedMinutes,
          value: parsedValue,
        })
      } else {
        await parkingTaxService.create({
          type: form.type,
          minutes: parsedMinutes,
          value: parsedValue,
        })
      }

      navigate('/parkingtax')
    } catch (submitError) {
      if (submitError instanceof Error) {
        setError(submitError.message)
      } else {
        setError('Não foi possível salvar a tarifa.')
      }
    } finally {
      setSubmitting(false)
    }
  }

  return (
    <section className="section">
      <header className="section-header">
        <div>
          <h2>{isEditing ? 'Editar tarifa' : 'Nova tarifa'}</h2>
          <p>{isEditing ? 'Atualize os dados da tarifa.' : 'Cadastre uma nova tarifa.'}</p>
        </div>
        <Link className="btn btn-secondary" to="/parkingtax">
          Voltar para lista
        </Link>
      </header>

      {loadingRecord ? <LoadingMessage message="Carregando tarifa..." /> : null}

      {!loadingRecord ? (
        <article className="form-card">
          <form className="stack" onSubmit={(e) => void handleSubmit(e)}>
            <div className="field-group">
              <label htmlFor="type">Tipo</label>
              <select
                id="type"
                className="input-control"
                value={form.type}
                onChange={(e) => handleTypeChange(e.target.value as ParkingTaxType)}
              >
                {TAX_TYPES.map((type) => (
                  <option key={type} value={type}>
                    {PARKING_TAX_TYPE_LABELS[type]}
                  </option>
                ))}
              </select>
            </div>

            {form.type === 'Hour' ? (
              <Input
                id="minutes"
                label="Tempo Até (minutos)"
                type="number"
                min={1}
                value={form.minutes}
                onChange={(e) => setForm((prev) => ({ ...prev, minutes: e.target.value }))}
                placeholder="60"
              />
            ) : null}

            <Input
              id="value"
              label="Valor (R$)"
              type="number"
              min={0.01}
              step={0.01}
              value={form.value}
              onChange={(e) => setForm((prev) => ({ ...prev, value: e.target.value }))}
              placeholder="10.00"
            />

            {error ? <ErrorMessage message={error} /> : null}

            <Button type="submit" disabled={submitting}>
              {submitting ? 'Salvando...' : isEditing ? 'Salvar alterações' : 'Cadastrar tarifa'}
            </Button>
          </form>
        </article>
      ) : null}
    </section>
  )
}
