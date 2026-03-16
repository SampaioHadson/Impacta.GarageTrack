import type { InputHTMLAttributes } from 'react'

interface InputProps extends InputHTMLAttributes<HTMLInputElement> {
  label: string
  error?: string | null
}

export function Input({ label, id, error, className, ...props }: InputProps) {
  const classes = ['input-control', error ? 'input-error' : '', className ?? ''].join(' ').trim()

  return (
    <div className="field-group">
      <label htmlFor={id}>{label}</label>
      <input id={id} className={classes} {...props} />
      {error ? <span className="field-error">{error}</span> : null}
    </div>
  )
}
