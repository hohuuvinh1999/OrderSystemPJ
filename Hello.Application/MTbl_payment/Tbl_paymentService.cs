﻿using Hello.Application.MTbl_payment;
using Hello.Data;
using Hello.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocDotNet.Application.MTbl_payment
{
	public class Tbl_paymentService : ITbl_paymentService
	{
		private readonly HelloDbContext _context;

		public Tbl_paymentService(HelloDbContext context)
		{
			_context = context;
		}

		public async Task<int> Create(Tbl_paymentRequest tbl_paymentRequest)
		{
			int maxIdOrder = _context.tbl_orders.Max(u => u.id);
			var tbl_payment = new tbl_payment()
			{
				receive = tbl_paymentRequest.receive,
				refund = tbl_paymentRequest.refund,
				type = tbl_paymentRequest.type,
				idorder = maxIdOrder
			};
			_context.tbl_payments.Add(tbl_payment);

			return await _context.SaveChangesAsync();
		}

		public async Task<int> Delete(Tbl_paymentRequest tbl_paymentRequest)
		{
			var tbl_payment = new tbl_payment()
			{
				id = tbl_paymentRequest.id
			};
			_context.tbl_payments.Remove(tbl_payment);

			return await _context.SaveChangesAsync();
		}


		public async Task<int> Update(Tbl_paymentRequest tbl_paymentRequest)
		{
			int maxIdOrder = _context.tbl_orders.Max(u => u.id);
			var tbl_payment = new tbl_payment()
			{
				id = tbl_paymentRequest.id,
				receive = tbl_paymentRequest.receive,
				refund = tbl_paymentRequest.refund,
				type = tbl_paymentRequest.type,
				idorder = maxIdOrder
			};
			_context.tbl_payments.Update(tbl_payment);

			return await _context.SaveChangesAsync();
		}

		public async Task<List<Tbl_paymentResponse>> GetAll()
		{
			return await _context.tbl_payments
				.Select(x => new Tbl_paymentResponse()
				{
					id = x.id,
					receive = x.receive,
					refund = x.refund,
					type = x.type,
					idorder = x.idorder
				}).ToListAsync();
		}

	}
}