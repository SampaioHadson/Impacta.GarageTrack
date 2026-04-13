import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom'
import { ProtectedRoute } from '../components/auth/ProtectedRoute'
import { AppShell } from '../components/layout/AppShell'
import { LoginPage } from '../pages/LoginPage'
import { ParkingCreatePage } from '../pages/parking/ParkingCreatePage'
import { ParkingDetailPage } from '../pages/parking/ParkingDetailPage'
import { ParkingListPage } from '../pages/parking/ParkingListPage'
import { ParkingTaxFormPage } from '../pages/parkingTax/ParkingTaxFormPage'
import { ParkingTaxListPage } from '../pages/parkingTax/ParkingTaxListPage'

export function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<LoginPage />} />

        <Route
          element={
            <ProtectedRoute>
              <AppShell />
            </ProtectedRoute>
          }
        >
          <Route index element={<Navigate to="/parking" replace />} />
          <Route path="/parking" element={<ParkingListPage />} />
          <Route path="/parking/new" element={<ParkingCreatePage />} />
          <Route path="/parking/:id" element={<ParkingDetailPage />} />
          <Route path="/parkingtax" element={<ParkingTaxListPage />} />
          <Route path="/parkingtax/new" element={<ParkingTaxFormPage />} />
          <Route path="/parkingtax/:id/edit" element={<ParkingTaxFormPage />} />
        </Route>

        <Route path="*" element={<Navigate to="/parking" replace />} />
      </Routes>
    </BrowserRouter>
  )
}
