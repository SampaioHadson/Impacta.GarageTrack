interface FeedbackProps {
  message: string
}

export function LoadingMessage({ message }: FeedbackProps) {
  return <p className="feedback loading">{message}</p>
}

export function ErrorMessage({ message }: FeedbackProps) {
  return <p className="feedback error">{message}</p>
}
