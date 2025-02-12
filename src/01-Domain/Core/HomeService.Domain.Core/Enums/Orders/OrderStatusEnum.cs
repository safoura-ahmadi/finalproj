﻿using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Enums.Orders;
public enum OrderStatusEnum
{
    [Display(Name = "در انتظار پیشنهاد متخصص")]
    WaitingForExpertOffer = 0,
    [Display(Name = "در انتظار انتخاب متخصص")]
    WaitingForExpertSelection,
    [Display(Name = "امدن کارشناس به محل شما")]
    ExpertArrivedAtLocation,
    [Display(Name = "اتمام کار و پرداخت")]
    WorkCompletedAndPaid
}
