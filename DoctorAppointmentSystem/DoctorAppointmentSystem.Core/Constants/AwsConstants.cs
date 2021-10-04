using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Constants
{
    public static class AwsConstants
    {
        public const string BucketName = "doctorappointmentbucket";
        public const string TableName = "appointment_table";
        public const string BucketImageUrl = "https://doctorappointmentbucket.s3.ap-southeast-1.amazonaws.com/";
    }
}
