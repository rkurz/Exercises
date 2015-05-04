/*
Question 5:
  - List the names of the employees who did not receive a pay raise in January of 2014. 
*/
select
  e.id,
  e.name,
  currentSalary.amount
from employee e
  left outer join salary currentSalary on e.id = currentSalary.employee_id	-- employees current salary on dec 31
    and currentSalary.effective_date <= '12/31/2013'
    and currentSalary.expiration_date >= '12/31/2013'
where not exists (select 
                    s.amount 
                  from 
                    salary s
                  where
                    s.employee_id = e.id
                    and s.effective_date >= '01/01/2014'	-- look for salary records that take effect in January
                    and s.effective_date <= '01/31/2014'
                    and s.amount > currentSalary.amount)	