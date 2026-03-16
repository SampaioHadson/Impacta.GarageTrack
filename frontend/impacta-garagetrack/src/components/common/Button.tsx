import type { ButtonHTMLAttributes, ReactNode } from 'react'

interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
  children: ReactNode
  fullWidth?: boolean
  variant?: 'primary' | 'secondary'
}

export function Button({
  children,
  fullWidth = false,
  variant = 'primary',
  className,
  ...props
}: ButtonProps) {
  const classes = ['btn', `btn-${variant}`, fullWidth ? 'btn-full' : '', className ?? '']
    .join(' ')
    .trim()

  return (
    <button className={classes} {...props}>
      {children}
    </button>
  )
}
