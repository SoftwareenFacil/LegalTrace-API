using LegalTrace.SMTP.Models;

namespace LegalTrace.SMTP
{
    public static class PendingChargeEmailBuilder
    {
        public static string Build(
            ChargeData chargeData,
            MailData mailData,
            // Payment Info
            AccountData accountData,
            // Footer
            SenderData senderData
        )
        {
            return $@"
<!DOCTYPE html>
<html lang=""es"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>{mailData.MainHeading}</title>
</head>

<body style=""font-family: Arial, sans-serif; background-color: #f5f7fa; margin: 0; padding: 20px;"">
    <div style=""max-width: 650px; margin: auto; background: #ffffff; padding: 32px; border-radius: 8px; box-shadow: 0 2px 6px rgba(0,0,0,0.1);"">

        <!-- Header -->
        <div style=""margin-bottom: 32px;"">
            <div style=""display: flex; align-items: center; gap: 12px; margin-bottom: 8px;"">
                <div style=""width: 42px; height: 42px; background-color: #f59e0b; border-radius: 9999px; display: flex; align-items: center; justify-content: center;"">
                    <span style=""font-size: 20px; color: white;"">&#128221;</span>
                </div>

                <div>
                    <div style=""font-size: 13px; font-weight: bold; color: #b45309;"">{senderData.SystemShortName}</div>
                    <div style=""font-size: 11px; color: #6b7280;"">{senderData.SystemFullName}</div>
                </div>
            </div>

            <h3 style=""font-size: 26px; font-weight: bold; color: #111827; margin: 16px 0 8px 0;"">{mailData.MainHeading}</h3>
            <div style=""height: 4px; width: 60px; background-color: #059669; border-radius: 4px;""></div>
        </div>

        <!-- Greeting -->
        <p style=""color: #374151; margin-bottom: 24px;"">Estimado/a {chargeData.companyName},</p>

        <p style=""color: #374151; margin-bottom: 24px;"">{mailData.MainBody}</p>

        <!-- Charge Box -->
        <div style=""background-color: #ecfdf5; border-left: 4px solid #059669; padding: 24px; margin-bottom: 24px;"">
            <h4 style=""font-weight: bold; color: #111827; font-size: 20px; margin-bottom: 16px;"">{chargeData.chargeTitle}</h4>

            <div style=""margin-bottom: 16px;"">
                <p style=""font-size: 14px; color: #6b7280; margin-bottom: 4px;"">Descripción</p>
                <p style=""color: #1f2937;"">{chargeData.description}</p>
            </div>

            <div style=""display: grid; grid-template-columns: 1fr 1fr; gap: 16px; padding-top: 12px; border-top: 1px solid #a7f3d0;"">
                <div>
                    <p style=""font-size: 14px; color: #6b7280;"">Tipo de Cargo</p>
                    <p style=""font-weight: 600; color: #111827;"">{chargeData.chargeType}</p>
                </div>

                <div>
                    <p style=""font-size: 14px; color: #6b7280;"">Fecha de Emisión</p>
                    <p style=""font-weight: 600; color: #111827;"">{chargeData.emissionDate}</p>
                </div>
            </div>

            <div style=""padding-top: 12px; border-top: 1px solid #a7f3d0;"">
                <p style=""font-size: 14px; color: #6b7280; margin-bottom: 4px;"">Monto Total</p>
                <p style=""font-weight: 600; color: #111827;"">{chargeData.totalAmount}</p>
            </div>
        </div>

        <!-- Payment Info -->
        <div style=""background-color: #f9fafb; border: 1px solid #e5e7eb; border-radius: 8px; padding: 24px; margin-bottom: 24px;"">
            <h4 style=""font-weight: 600; color: #111827; margin-bottom: 16px;"">Información de Pago</h4>

            <div style=""display: grid; grid-template-columns: 1fr 1fr; gap: 16px; font-size: 14px;"">
                <div>
                    <p style=""color: #6b7280;"">N° Cuenta</p>
                    <p style=""font-weight: 500; color: #111827;"">{accountData.AccountNumber}</p>
                </div>

                <div>
                    <p style=""color: #6b7280;"">Tipo de Cuenta</p>
                    <p style=""font-weight: 500; color: #111827;"">{accountData.AccountType}</p>
                </div>

                <div>
                    <p style=""color: #6b7280;"">Nombre Banco</p>
                    <p style=""font-weight: 500; color: #111827;"">{accountData.BankName}</p>
                </div>

                <div>
                    <p style=""color: #6b7280;"">Rut</p>
                    <p style=""font-weight: 500; color: #111827;"">{accountData.Rut}</p>
                </div>

                <div style=""grid-column: span 2;"">
                    <p style=""color: #6b7280;"">Nombre Cuenta</p>
                    <p style=""font-weight: 500; color: #111827;"">{accountData.AccountName}</p>
                </div>
            </div>
        </div>

        <!-- Instructions -->
        <div style=""background-color: #eff6ff; border: 1px solid #bfdbfe; border-radius: 8px; padding: 16px; margin-bottom: 24px;"">
            <p style=""font-size: 14px; color: #1e40af;"">
                <strong>Instrucciones de Pago:</strong>
                Por favor, realice el pago utilizando los datos bancarios proporcionados. 
                Una vez realizado el pago, el comprobante será generado y enviado a su correo electrónico.
            </p>
        </div>

        <p style=""color: #374151; margin-bottom: 12px;"">Si tiene alguna duda o requiere información adicional, no dude en contactarnos.</p>

        <p style=""color: #374151; margin-bottom: 4px;"">Atentamente,</p>
        <p style=""font-weight: 600; color: #111827; margin-bottom: 2px;"">{senderData.TeamName}</p>
        <p style=""font-size: 14px; color: #6b7280;"">{senderData.SystemShortName}</p>
        <p style=""font-size: 14px; color: #6b7280;"">{senderData.SystemFullName}</p>

        <!-- Footer -->
        <div style=""margin-top: 32px; padding-top: 24px; border-top: 1px solid #e5e7eb;"">
            <p style=""font-size: 11px; color: #6b7280;"">
                Este es un correo automático generado por el sistema. Por favor, no responder directamente a este mensaje.
                Para cualquier consulta, utilice los canales de comunicación oficiales.
            </p>
        </div>
    </div>
</body>
</html>";
        }
    }
}
