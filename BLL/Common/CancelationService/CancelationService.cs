using BLLServices.Common.EmailService;
using BLLServices.Managers.AppointmentManager;
using BLLServices.Managers.DoctorReservationManager;
using BLLServices.Managers.OrderManager;
using BLLServices.Payment;
using BLLServices.Payment.DTOs;
using DAL.Models;

namespace BLLServices.Common.CancelationService
{
    public class CancelationService : ICancelationService
    {
        private IDoctorReservationManager doctorReservationManager;
        private IAppointmentManager appointmentManager;
        private IEmailService emailService;
        private readonly IOrderManager orderManager;
        private readonly PayPalClient payPalClient;

        public CancelationService(IDoctorReservationManager doctorReservationManager,IAppointmentManager appointmentManager
                                              ,IEmailService emailService, IOrderManager orderManager, PayPalClient payPalClient) 
        {
            this.doctorReservationManager = doctorReservationManager;
            this.appointmentManager = appointmentManager;
            this.emailService = emailService;
            this.orderManager = orderManager;
            this.payPalClient = payPalClient;
        }
        public async Task CancelDoctorReservation(int resId)
        {
            var Res = doctorReservationManager.GetDoctorReservationByID(resId).Result;
            if (Res != null)
            {
                var appointments = appointmentManager.GetAppointmentsByReservationId(resId).Result;
                foreach (var appointment in appointments)
                {
                    Order? order = orderManager.GetOrder(appointment.PatientId, (int)appointment.DoctorReservationID!);
                    if (order != null)
                    {
                        if (order.Status && order.CaptureId != null)
                            await payPalClient.RefundOrder(order.CaptureId);
                        orderManager.DeleteOrder(appointment.PatientId, (int)appointment.DoctorReservationID!);
                    }
                    var name = $"{appointment.Patient.AppUser.FirstName} {appointment.Patient.AppUser.LastName}" ;
                    emailService.SendEmail(new Email()
                    {
                        Subject = "Reservation Cancellation",
                        Template = MailTemplates.CancelAppointmentTemplate,
                        To = appointment.Patient.AppUser.Email,
                        Link = $"https://doc-net.runasp.net/"
                    }, name,
                     $"<!DOCTYPE html><html><head><meta charset=\"UTF-8\"><title>Appointment Cancellation</title><style>body{{font-family:Arial,sans-serif;background-color:#f4f4f7;margin:0;padding:0}}.container{{max-width:600px;margin:auto;background:#ffffff;padding:20px;border-radius:8px;box-shadow:0 2px 4px rgba(0,0,0,0.1)}}.header{{padding-bottom:20px}}.header img{{width:150px}}.button{{background-color:#FF4C4C;color:white !important;padding:12px 20px;text-decoration:none;border-radius:5px;display:inline-block;margin-top:20px}}.footer{{text-align:center;font-size:12px;color:#888;margin-top:20px}}</style></head><body><div class=\"container\"><div class=\"header\"><img src=\"https://i.postimg.cc/L5B2kZVq/Whats-App-Image-2025-04-12-at-21-33-09-1c5f71b2.jpg\" alt=\"DocNet Logo\"></div><h2>Appointment Cancellation</h2><p>Hello {name},</p><p>We regret to inform you that your scheduled appointment on <strong>{appointment.DoctorReservation.StartTime.Date}</strong> has been canceled.And your payment had beed refunded. We apologize for any inconvenience this may cause.</p><p>If you'd like to reschedule, please visit our website or contact our support team for assistance.</p><a href=\"https://doc-net.runasp.net/\" class=\"button\">visit the website</a><p>Thank you for your understanding, and we apologize for any disruption this may cause.</p><div class=\"footer\">&copy; 2025 ITI Student Team. All rights reserved.</div></div></body></html>");
                    //appointment.DoctorReservationID = null;
                    //appointmentManager.UpdateAppointment(appointment);
                    appointmentManager.DeleteAppointmentAsync(appointment.ID);
                }
                doctorReservationManager.DeleteDoctorReservation(Res);
            }
            else
            {
                throw new Exception("Reservation not found");
            }


        }
    }
}
