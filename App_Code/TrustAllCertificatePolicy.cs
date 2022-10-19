using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
{
    public TrustAllCertificatePolicy()
    {
    }
    public bool CheckValidationResult(System.Net.ServicePoint srvPoint, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Net.WebRequest request, int certificateProblem)
    {
        return true;
    }
}