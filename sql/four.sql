/*
Question 4:
  - What is the total salary paid from 10/1/2013 to 09/30/2014? 
  
Notes:
  - count the number of days in the effective range that fall into the target date range
  - amount/365 = salary per day
  - number of days * salary per day = total salary
*/

declare @rangeStart date = '10/1/2013';
declare @rangeFinish date = '09/30/2014';

with salaryWithAdjustedDates as
(
	select
	  s.amount as amountForYear,
	  s.amount / 365 as amountForDay,
	  case when s.effective_date < @rangeStart then @rangeStart else s.effective_date end as adjustedEffectiveDate,		-- Only include the days within the target date range.
	  case when s.expiration_date > @rangeFinish then @rangeFinish else s.expiration_date end as adjustedExpirationDate
	from
	  salary s
	where
	  s.effective_date <= @rangeFinish
	  and s.expiration_date >= @rangeStart
)
select
  SUM((DATEDIFF(DAY, adjustedEffectiveDate, adjustedExpirationDate) + 1) * s.amountForDay) as total_salary
from salaryWithAdjustedDates s